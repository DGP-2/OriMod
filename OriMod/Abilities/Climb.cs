using System;
using System.IO;
using Terraria.ModLoader;

namespace OriMod.Abilities {
  /// <summary>
  /// Ability for climbing on walls.
  /// </summary>
  public sealed class Climb : Ability, ILevelable {
    internal Climb(AbilityManager manager) : base(manager) { }
    public override int Id => AbilityID.Climb;
    public override byte Level => (this as ILevelable).Level;
    byte ILevelable.Level { get; set; }
    byte ILevelable.MaxLevel => 1;

    internal override bool CanUse => base.CanUse && oPlayer.OnWall && !oPlayer.IsGrounded && !player.mount.Active && !abilities.wallJump && !abilities.wallChargeJump;

    internal bool IsCharging {
      get => _isCharging;
      private set {
        if (value != _isCharging) {
          _isCharging = value;
          netUpdate = true;
        }
      }
    }
    private bool _isCharging;

    internal sbyte wallDirection;

    protected override void ReadPacket(BinaryReader r) {
      wallDirection = r.ReadSByte();
      IsCharging = r.ReadBoolean();
    }

    protected override void WritePacket(ModPacket packet) {
      packet.Write(wallDirection);
      packet.Write(IsCharging);
    }

    protected override void UpdateActive() {
      if (IsCharging) {
        player.velocity.Y = 0;
      }
      else {
        if (player.controlUp) {
          player.velocity.Y += player.velocity.Y < (player.gravDir > 0 ? -2 : 4) ? 1 : -1;
        }
        else if (player.controlDown) {
          player.velocity.Y += player.velocity.Y < (player.gravDir > 0 ? 4 : -2) ? 1 : -1;
        }
        if (!player.controlDown && !player.controlUp) {
          player.velocity.Y *= Math.Abs(player.velocity.Y) > 1 ? 0.35f : 0;
        }
      }

      player.gravity = 0;
      player.runAcceleration = 0;
      player.maxRunSpeed = 0;
      player.direction = wallDirection;
      player.velocity.X = 0;
      player.controlLeft = false;
      player.controlRight = false;
      player.controlUp = false;
    }

    internal override void Tick() {
      if (!IsLocal) {
        return;
      }
      if (!InUse) {
        if (CanUse && OriMod.ClimbKey.Current) {
          SetState(State.Active);
          wallDirection = (sbyte)player.direction;
        }
      }
      else if (!CanUse || !OriMod.ClimbKey.Current) {
        SetState(State.Inactive);
      }
      IsCharging = Active && abilities.wallChargeJump.Unlocked && (wallDirection == 1 ? player.controlLeft : player.controlRight);
    }
  }
}