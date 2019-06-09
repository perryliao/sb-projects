using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Antique : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 23233;
        [Configurable]
        public double endTime = 34527;

        private double beatLength = 23939 - 23233;

        // private int width = 854;
        // private int height = 480;

        public override void Generate()
        {
		    var layer = GetLayer("Antique");
            OsbSprite mask = layer.CreateSprite("sb/Pool 2/Border Mask.png", OsbOrigin.Centre);

            // Layer mask
            mask.Fade(startTime, 0.8);
            mask.Color(startTime, 0, 0, 0);
            mask.ScaleVec(startTime, mask.ScaleAt(startTime).X, mask.ScaleAt(startTime).Y * 0.7);
            mask.Scale(startTime, startTime + 4*(beatLength * 4), 1, 0.75);
            mask.Fade(endTime, 0);

            // vertical line effect
            Random rnd = new Random();
            OsbSprite line = configureLineEffect(startTime, endTime);
        }

        /// <summary>This is where line effect sprites are created, It is intended to set states common to every sprite.</summary>
        /// <param name="lineStartTime">Time when the line will appear</param>
        /// <param name="lineEndTime">Time when the line will fade out</param>
        /// 
        private OsbSprite configureLineEffect(double lineStartTime, double lineEndTime) {
            OsbSprite line = GetLayer("Antique").CreateSprite("sb/Pool 1/rain.png", OsbOrigin.Centre);
            line.Fade(lineStartTime, 0.4);
            line.ScaleVec(lineStartTime, 1, 15);
            line.ColorHsb(lineStartTime, 0, 0, 0.05);
            line.Fade(lineEndTime, 0);
            return line;
        }
    }
}
