using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLFactoryBase
    {

        private const string POINT_COLLECTION_START = "(";
        private const string POINT_COLLECTION_END = ")";
        protected const string graphic_line = @"GRAPHIC_LINE";
        private const string track_1_ident = @"TRACK_1";
        private const string track_ident = @"TRACK: ";
        private const string graphic_point = @"GRAPHIC_POINT";


        #region Node parsing Methods
        protected static bool ParseTrack1(string line,ILDLBaseNode nodeBO)
        {
            if (line.Contains(track_1_ident))
            {
                nodeBO.Track1Str = ParseItem(line);
                return true;
            }
            if (line.Contains(track_ident))//end and boundary nodes don't have a number but I've kept it the same
            {
                nodeBO.Track1Str = ParseItem(line);
                return true;
            }
            return false;
        }

        protected static bool ParseGraphicPoint(string line, ILDLHasGraphicLocation nodeBO)
        {
            if (nodeBO == null)
                throw new ArgumentNullException("Node Business Object must be set");
            if (line.Contains(graphic_point))
            {
                int pointStart = line.IndexOf(LDLSeperators.LINE_SEPERATOR);
                int pointEnd = line.IndexOf(LDLPoint.POINT_END);
                string pointDef = line.Substring(pointStart, pointEnd - pointStart);
                nodeBO.GraphicPoint = new LDLPoint(pointDef);
                return true;
            }
            return false;
        }
        #endregion

        protected static string ParseItem(string line)
        {
            int start = line.IndexOf(LDLSeperators.LINE_SEPERATOR)+1;//we don't want to include the separator
            int end = line.IndexOf(LDLSeperators.TERMINATER);
            if (end <= 0)
                end = line.IndexOf(LDLSeperators.MEASUREMENT_SEPERATOR); //multi line values won't have a terminator
            int firstSpeachMark = line.IndexOf('\"');
            if (firstSpeachMark > 0)
                start = firstSpeachMark;
            return line.Substring(start, end - start).Replace("\"", "").Trim();            
        }

        protected static void CreateGraphicLine(string[] definition, LDLTrack result, int graphicLineStartIndex, int nLines)
        {
            int graphicLineEndIdx = -1;
            for (int idx = graphicLineStartIndex; idx < nLines; idx++)
            {
                if (definition[idx].Contains(LDLSeperators.TERMINATER))
                {
                    graphicLineEndIdx = idx;
                    break;
                }
            }
            if (graphicLineEndIdx > 0)//we have a properly defined line
            {
                definition[graphicLineStartIndex] = definition[graphicLineStartIndex].Replace(POINT_COLLECTION_START, "");
                definition[graphicLineStartIndex] = definition[graphicLineStartIndex].Replace(graphic_line, "");
                definition[graphicLineStartIndex] = definition[graphicLineStartIndex].Replace(LDLSeperators.LINE_SEPERATOR, "");
                definition[graphicLineEndIdx] = definition[graphicLineEndIdx].Replace(POINT_COLLECTION_END, "");
                definition[graphicLineEndIdx] = definition[graphicLineEndIdx].Replace(LDLSeperators.TERMINATER, "");
                result.Graphic_Line = new LDLGraphicLine();


                for (int idx = graphicLineStartIndex; idx <= graphicLineEndIdx; idx++)
                {

                    string pointToProcess = definition[idx].Remove(definition[idx].Length - 1);//on the last line this will be removing a space, otherwise, remove the comma
                    pointToProcess = pointToProcess.Trim();
                    result.Graphic_Line.Points.Add(new LDLPoint(pointToProcess));


                }
            }
        }
    }
}
