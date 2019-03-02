using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeOnline
{
    class Problem
    {
        public int N;
       

        public Photo[] photos;

        public List<Tag> tags;

        public List<Slide> slides;

        public Problem()
        {
            
        }

        public void Solve(string outFileName)
        {
            //CreateSlidesByTags();
            CreateSlidesByPhoto();
            
            Output(outFileName);
        }

        private void CreateSlidesByTags()
        {
            var SortedTags = tags.OrderByDescending(t => t.pc);
            Photo vert1 = null, vert2 = null;

            foreach (Tag t in SortedTags)
            {
                

                foreach(int p in t.photos)
                {
                    var ph = photos[p];

                    

                    if(ph.H_V == "H" && !ph.taken)
                    {
                        ph.taken = true;

                        var slide = new Slide();

                        slide.photos.Add(p);

                        slides.Add(slide);
                    }
                    else
                    {
                        if (vert1 == null && !ph.taken)
                        {
                            vert1 = ph;
                            vert1.taken = true;
                        }
                        else if (vert2 == null && !ph.taken)
                        {
                            vert2 = ph;

                            var slide = new Slide();

                            slide.photos.Add(vert1.i);
                            slide.photos.Add(vert2.i);

                            slides.Add(slide);

                            vert2.taken = true;

                            vert1 = null;
                            vert2 = null;
                        }
                    }
                }
                
            }
        }

        private void CreateSlidesByPhoto()
        {
            List<Photo> photosList;

            Photo vert1 = null, vert2 = null;


            foreach (Photo p in photos)
            {
                var orderedTags = p.tags.OrderBy(t => t);

                foreach (string t in orderedTags)
                {
                    p.concatTags += t + " ";
                }
            }

            photosList = photos.ToList();

            var orderedPhotos = photosList.OrderBy(p => p.H_V).ThenByDescending(p => p.tags.Count).ThenBy(p => p.concatTags);

            foreach(Photo ph in orderedPhotos)
            {
                if (ph.H_V == "H" && !ph.taken)
                {
                    ph.taken = true;

                    var slide = new Slide();

                    slide.photos.Add(ph.i);

                    slides.Add(slide);
                }
                else
                {
                    if (vert1 == null && !ph.taken)
                    {
                        vert1 = ph;
                        vert1.taken = true;
                    }
                    else if (vert2 == null && !ph.taken)
                    {
                        vert2 = ph;

                        var slide = new Slide();

                        slide.photos.Add(vert1.i);
                        slide.photos.Add(vert2.i);

                        slides.Add(slide);

                        vert2.taken = true;

                        vert1 = null;
                        vert2 = null;
                    }
                }
                

            }


        }

        private void Output(string outFileName)
        {
            File.AppendAllText(outFileName, slides.Count() + Environment.NewLine);

            //Console.WriteLine(slides.Count());

            string photoOut = "";

            foreach (Slide s in slides)
            {
                
                foreach(int p in s.photos)
                {

                    photoOut += p + " ";
                }

                File.AppendAllText(outFileName, photoOut + Environment.NewLine);
                //Console.WriteLine(photoOut);

                photoOut = "";
            }
        }
    }

    class Slide
    {
        public Slide()
        {
            photos = new List<int>();
        }

        public List<int> photos;
    }

    class Tag
    {
        public string tagString;
        public int pc;
        public List<int> photos;
    }
    class Photo
    {
        public int i;
        public bool taken;
        public string H_V;
        public int M;
        public List<string> tags;
        public string concatTags;    
    }



}
