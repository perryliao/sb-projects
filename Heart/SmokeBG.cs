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
    public class SmokeBG : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 45821;
        [Configurable]
        public double endTime = 68409;
        [Configurable]
        public double opacity = 0.6;
        [Configurable]
        public int numSmokes = 4;

        private string[] smokeMap = new string[9] {
            "2",
            "4",
            "5",
            "6",
            "6b",
            "9",
            "10",
            "11",
            "14"
        };

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("SmokeBG");

            double i, curX, curY, mStart, mEnd, variation = 20;
            int j;
            OsbSprite smoke;
            string currentSmoke;
            double[] prevX = new double[numSmokes]; 
            double[] prevY = new double[numSmokes];
            Color4 fromColour, toColour;

            for (j = 0; j < numSmokes; j++) {
                prevX[j] = 320 + Random(-150, 150);
                prevY[j] = 240 + Random(-20, 20);
            }

            for (i = startTime; i < endTime; i += Constants.beatLength*4) {
                for (j = 0; j < numSmokes; j++) {
                    currentSmoke = smokeMap[Random(smokeMap.Length)];
                    fromColour = Constants.randomColours[Random(Constants.randomColours.Length)];
                    toColour = Constants.randomColours[Random(Constants.randomColours.Length)];
                    
                    curX = prevX[j] + Random(-variation, variation);
                    curY = prevY[j] + Random(-variation, variation);
                    while(curX < Constants.xFloor || curX > Constants.xCeil)
                        curX = prevX[j] + Random(-variation, variation);
                    while(curY < 0 || curY > Constants.height)
                        curY = prevY[j] + Random(-variation, variation);

                    mStart = i - Constants.beatLength/2;
                    mEnd = i + Constants.beatLength*8/2;
                    
                    // Time specific actions
                    if (mStart < startTime) {
                        mStart = startTime;
                    }
                    if (mEnd > endTime) {
                        mEnd = endTime - Constants.beatLength/4;
                        toColour = Constants.white;
                        currentSmoke = j > 0 ? "4" : currentSmoke;
                    } 
                    if (i == 54293) {
                        fromColour = Constants.darkRed;
                        toColour = Constants.darkRed;
                    } else if ( i == 65589 ) {
                    }

                    smoke = layer.CreateSprite($"sb/Pool 5/Smoke{currentSmoke}.png", OsbOrigin.Centre);
                    
                    smoke.Fade(OsbEasing.Out, mStart, mStart + Constants.beatLength, 0, opacity);
                    smoke.Fade(OsbEasing.In, mEnd - Constants.beatLength/2, mEnd, opacity, 0);
                    smoke.Scale(mStart, mEnd, 3, 3.2);
                    smoke.Rotate(mStart, mEnd, 0, Random(-Math.PI/32, Math.PI/32));

                    smoke.Move(mStart, mEnd, 
                        prevX[j],
                        prevY[j],
                        curX,
                        curY
                    );

                    
                    smoke.Color(mStart, mEnd, fromColour, toColour);

                    prevX[j] = curX;
                    prevY[j] = curY;
                }
            }        
        }
    }
}
