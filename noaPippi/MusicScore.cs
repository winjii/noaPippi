using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace noaPippi
{
    /// <summary>
    /// 音符を表示する辺りの領域を担当するクラス
    /// </summary>
    class MusicScore : SeparatedComponent
    {
        static Dictionary<string, Note.PitchName> pitchNameDictionary;
        static MusicScore()
        {
            pitchNameDictionary = new Dictionary<string, Note.PitchName>();
            pitchNameDictionary["C"] = Note.PitchName.C;
            pitchNameDictionary["D"] = Note.PitchName.D;
            pitchNameDictionary["E"] = Note.PitchName.E;
            pitchNameDictionary["F"] = Note.PitchName.F;
            pitchNameDictionary["G"] = Note.PitchName.G;
            pitchNameDictionary["A"] = Note.PitchName.A;
            pitchNameDictionary["B"] = Note.PitchName.B;
        }

        List<List<MeasurableElement>> measuresG;
        List<List<MeasurableElement>> measuresF;
        StaffNotation[] staffNotationG;
        StaffNotation[] staffNotationF;
        public MusicScore(Game game, RelativeViewport viewport) : base(game, viewport)
        {
            measuresG = new List<List<MeasurableElement>>();
            measuresF = new List<List<MeasurableElement>>();
            viewport.IsHorizontal = false;
            staffNotationG = new StaffNotation[4];
            staffNotationF = new StaffNotation[4];
            for (int i = 0; i < 4; i++)
            {
                RelativeViewport vG = viewport.AddFormedChild(1/8.0, true, false);
                RelativeViewport vF = viewport.AddFormedChild(1/8.0, true, false);
                staffNotationG[i] = new StaffNotation(Clef.ClefType.G, game, vG);
                staffNotationF[i] = new StaffNotation(Clef.ClefType.F, game, vF);
                game.Components.Add(staffNotationG[i]);
                game.Components.Add(staffNotationF[i]);
            }
        }
        public void Load(XmlDocument doc)
        {
            XmlNodeList measures = doc.SelectNodes("score-partwise/part/measure");
            XmlNode attributes = measures[0]["attributes"];
            int divisions = int.Parse(attributes["divisions"].InnerText);
            //TODO: 調に対応
            //XmlNode noteNumber = attributes.SelectSingleNode("noteNumber");
            //TODO: 拍子に対応
            //XmlNode time = attributes.SelectSingleNode("time");
            foreach (XmlNode measure in measures)
            {
                if (measure.Name != "measure") continue;
                List<MeasurableElement> measureG = new List<MeasurableElement>();
                List<MeasurableElement> measureF = new List<MeasurableElement>();
                for (int noteIndex = 0; noteIndex < measure.ChildNodes.Count; noteIndex++)
                {
                    XmlNode note = measure.ChildNodes[noteIndex];
                    XmlNode nextNote = (noteIndex + 1 < measure.ChildNodes.Count) ?
                        measure.ChildNodes[noteIndex + 1] :
                        null;
                    if (note.Name != "note") continue;
                    int duration = int.Parse(note["duration"].InnerText);
                    bool isDotted = false;
                    int pow = 0;
                    for (int i = 0; ; i++)
                    {
                        int d = 4*divisions/(1 << i);
                        if (d*3 == duration*2) isDotted = true;
                        if (d <= duration)
                        {
                            pow = i;
                            break;
                        }
                    }
                    MeasurableElement formedNote;
                    if (note["rest"] == null)
                    {
                        XmlNode pitch = note["pitch"];
                        Note.PitchName pitchName = pitchNameDictionary[pitch["step"].InnerText];
                        int octave = int.Parse(pitch["octave"].InnerText);
                        Note.Accidentals accidental = Note.Accidentals.None;
                        if (note["accidental"] != null)
                        {
                            string accidentalStr = note["accidental"].InnerText;
                            switch (accidentalStr)
                            {
                                case "sharp":
                                    accidental = Note.Accidentals.Sharp;
                                    break;
                                case "natural":
                                    accidental = Note.Accidentals.Natural;
                                    break;
                                case "flat":
                                    accidental = Note.Accidentals.Flat;
                                    break;
                            }
                        }
                        bool isOverlaid = (nextNote != null && nextNote["chord"] != null);
                        formedNote = new Note((Note.NoteType)pow, pitchName, octave, isDotted, accidental, isOverlaid);
                    }
                    else
                    {
                        formedNote = new Rest((Rest.RestType)pow);
                    }
                    int staff = int.Parse(note["staff"].InnerText);
                    if (staff == 1)
                    {
                        measureG.Add(formedNote);
                    }
                    else if (staff == 2)
                    {
                        measureF.Add(formedNote);
                    }
                    else throw new NotImplementedException();
                }
                measuresG.Add(measureG);
                measuresF.Add(measureF);
            }
            for (int i = 0; i < 4; i++)
            {
                staffNotationG[i].Measures = measuresG.GetRange(i*4, 4);
                staffNotationF[i].Measures = measuresF.GetRange(i*4, 4);
            }
        }
        protected override void LoadContent()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@"D:\Data\Documents\Visual Studio 2017\Projects\noaPippi\lg-130981710.xml");
            }
            catch (Exception e)
            {
                throw e;
            }
            Load(doc);
            base.LoadContent();
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            /*
            Line2DRenderer test = Line2DRenderer.GetInstance();
            Vector2 p00 = new Vector2(0, 0);
            Vector2 p01 = new Vector2((float)viewport.GetAbsoluteWidth(), 0);
            Vector2 p10 = new Vector2(0, (float)viewport.GetAbsoluteHeight());
            Vector2 p11 = new Vector2((float)viewport.GetAbsoluteWidth(), (float)viewport.GetAbsoluteHeight());
            test.Draw(spriteBatch, p00, p01, 10, Color.Red);
            test.Draw(spriteBatch, p01, p11, 10, Color.Red);
            test.Draw(spriteBatch, p11, p10, 10, Color.Red);
            test.Draw(spriteBatch, p10, p00, 10, Color.Red);
            test.Draw(spriteBatch, p01 + new Vector2(-10, 10), p10 + new Vector2(10, -10), 20, Color.Black);
            */
        }
    }
}
