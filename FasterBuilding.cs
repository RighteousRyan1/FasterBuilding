using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;
using System.Threading;
using Terraria.ModLoader.Audio;

namespace FasterBuilding
{
	public class FasterBuilding : Mod
	{
        private bool stopTitleMusic;
        private ManualResetEvent titleMusicStopped;
        private int customTitleMusicSlot;
        public FasterBuilding() { ModContent.GetInstance<FasterBuilding>(); }
        private void TitleMusicIL(ILContext il)
        {
            ILCursor ilcursor = new ILCursor(il);
            ILCursor ilcursor2 = ilcursor;
            MoveType moveType = MoveType.After;
            Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
            array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdfld<Main>(i, "newMusic"));
            ilcursor2.GotoNext(moveType, array);
            ilcursor.EmitDelegate<Func<int, int>>(delegate (int newMusic)
            {
                if (newMusic != 6)
                {
                    return newMusic;
                }
                return customTitleMusicSlot;
            });
        }
        private void MenuMusicSet()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/Join");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(TitleMusicIL);
        }
        public override void PostSetupContent()
        {
            if (Main.rand.NextFloat() < 0.1f)
            {
                if (ModLoader.GetMod("TerrariaOverhaul") == null && ModLoader.GetMod("MusicForOnePointFour") == null && ModLoader.GetMod("ExpiryMode") == null)
                {
                    MenuMusicSet();
                }
            }
        }
        public override void Close()
        {
            int soundSlot = GetSoundSlot((SoundType)51, "Sounds/Music/Join");
            if (Utils.IndexInRange(Main.music, soundSlot))
            {
                Music music = Main.music[soundSlot];
                if (music != null && music.IsPlaying)
                {
                    Main.music[soundSlot].Stop(Microsoft.Xna.Framework.Audio.AudioStopOptions.Immediate);
                }
            }
            base.Close();
        }
        public override void Unload()
        {
            ManualResetEvent manualResetEvent = titleMusicStopped;
            if (manualResetEvent != null)
            {
                manualResetEvent.Set();
            }
            titleMusicStopped = null;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (stopTitleMusic || (!Main.gameMenu && customTitleMusicSlot != 6 && Main.ActivePlayerFileData != null && Main.ActiveWorldFileData != null))
            {
                music = 6;
                stopTitleMusic = true;
                customTitleMusicSlot = 6;
                Music music2 = GetMusic("Sounds/Music/Join");
                if (music2.IsPlaying)
                {
                    music2.Stop((Microsoft.Xna.Framework.Audio.AudioStopOptions)1);
                }
                titleMusicStopped?.Set();
                stopTitleMusic = false;
            }
        }
    }
}