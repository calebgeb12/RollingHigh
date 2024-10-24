using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schepens.UtilScripts
{
    public class HighScore
    {
        public string initials = "";
        public int score = 0;
       
    }
    
    public class HighScoreHandler
    {
        public List<HighScore> scores = new List<HighScore>();
        private string highScoreFilename = "highscores.txt";
        public int numScoresToKeep = 10;

        public HighScoreHandler()
        {
        }

        public void ResetScores()
        {
            scores.Clear();
        }

        public void AddScore(string initials, int score)
        {
            HighScore newScore = new HighScore();
            newScore.initials = initials;
            newScore.score = score;
            scores.Add(newScore);

            SortScores();
            TrimList();
        }

        public void WriteToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(highScoreFilename);  // Open file for writing

                // Write out each high score to file
                foreach (HighScore sc in scores)
                {
                    string msg = string.Format("{0}|{1}", sc.initials, sc.score);
                    sw.WriteLine(msg);
                }
                
                sw.Close();  //Close the file
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void ReadFromFile()
        {
            try
            {
                scores.Clear();

                StreamReader sr = new StreamReader(highScoreFilename);

                // Read lines from the file until the end of the file is reached.
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    String[] parts = line.Split('|');
                    string initials = parts[0];
                    int score = Convert.ToInt32(parts[1]);
                    AddScore(initials, score);
                }
                
                sr.Close();  //Close the file
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void TrimList()
        {
            if (scores.Count > numScoresToKeep)
            {
                int numToRemove = scores.Count - numScoresToKeep;
                scores.RemoveRange(numScoresToKeep, numToRemove);
            } 
        }

        private void SortScores()
        {
            scores = scores.OrderBy(o => o.score).ToList();
            scores.Reverse();
        }

        public string ToString()
        {
            string msg = "";

            foreach (HighScore sc in scores)
            {
                string tmp = string.Format("{0}|{1}\n\r", sc.initials, sc.score);
                msg += tmp;
            }

            msg += "===========================\n\r";
            msg += "";

            return msg;
        }
    }
}
