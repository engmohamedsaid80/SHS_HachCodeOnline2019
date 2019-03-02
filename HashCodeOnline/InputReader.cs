using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeOnline
{
    class InputReader
    {
        private string fileName;

        public InputReader(string fname)
        {
            this.fileName = fname;
        }

        public Problem ReadInput()
        {
            Problem p = new Problem();

            p.slides = new List<Slide>();

            string[] AllLines = File.ReadAllLines(fileName);

            int lineIndex = 0;
            //setup line
            string[] line = AllLines[lineIndex].Split(' ');

            p.N = int.Parse(line[0]);

            p.photos = new Photo[p.N];

            p.tags = new List<Tag>();

            Hashtable hashtable = new Hashtable();

            

            //EndPoints
            for (int e = 0; e < p.N; e++)
            {
                lineIndex++;
                line = AllLines[lineIndex].Split(' ');

                

                p.photos[e] = new Photo();

                p.photos[e].i = e;

                p.photos[e].taken = false;

                p.photos[e].H_V = line[0];

                p.photos[e].M = int.Parse(line[1]);

                p.photos[e].tags = new List<string>();

                int t = 2;
                //Photos
                for (int c = 0; c < p.photos[e].M; c++)
                {
                    var currTag = line[t++];

                    p.photos[e].tags.Add(currTag);

                    bool found = hashtable.ContainsKey(currTag);
                    //var tag = p.tags.Find(tg => tg.tagString == currTag);

                    if(!found)
                    {
                        //add tag
                        Tag tt = new Tag
                        {
                            tagString = currTag,
                            pc = 1,
                            photos = new List<int>(),

                        };

                        tt.photos.Add(e);

                        hashtable.Add(currTag, tt);

                        //p.tags.Add(tt);
                    }
                    else
                    {
                        var tt = hashtable[currTag] as Tag;
                        
                        tt.pc++;
                        tt.photos.Add(e);

                        hashtable[currTag] = tt;
                    }
                }
            }

            foreach(DictionaryEntry d in hashtable)
            {
                p.tags.Add(d.Value as Tag);
            }

            Console.WriteLine(fileName + " Read Complete");

            return p;
        }
    }
}
