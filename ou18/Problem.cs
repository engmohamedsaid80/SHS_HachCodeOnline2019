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
        private List<Slide> orderedSlides;

        public Problem()
        {
            
        }

        public void Solve(string outFileName)
        {
            //CreateSlidesByTags();
            CreateSlidesByPhoto();
            Output(outFileName);
        }

        //private void CreateSlidesByTags()
        //{
        //    var SortedTags = tags.OrderByDescending(t => t.pc);
        //    Photo vert1 = null, vert2 = null;

        //    foreach (Tag t in SortedTags)
        //    {
                

        //        foreach(int p in t.photos)
        //        {
        //            var ph = photos[p];

                    

        //            if(ph.H_V == "H" && !ph.taken)
        //            {
        //                ph.taken = true;

        //                var slide = new Slide();

        //                slide.photos.Add(p);

        //                slides.Add(slide);
        //            }
        //            else
        //            {
        //                if (vert1 == null && !ph.taken)
        //                {
        //                    vert1 = ph;
        //                    vert1.taken = true;
        //                }
        //                else if (vert2 == null && !ph.taken)
        //                {
        //                    vert2 = ph;

        //                    var slide = new Slide();

        //                    slide.photos.Add(vert1.i);
        //                    slide.photos.Add(vert2.i);

        //                    slides.Add(slide);

        //                    vert2.taken = true;

        //                    vert1 = null;
        //                    vert2 = null;
        //                }
        //            }
        //        }
                
        //    }
        //}

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

                    slide.photos.Add(ph);

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

                        slide.photos.Add(vert1);
                        slide.photos.Add(vert2);

                        slides.Add(slide);

                        vert2.taken = true;

                        vert1 = null;
                        vert2 = null;
                    }
                }
            }

            var unsortedSlides = slides;

            orderedSlides = new List<Slide>();

            int maxScore = 0;
            int maxIndex = 0;

            int sn = 0;
            Slide currSlide = slides[sn];
            orderedSlides.Add(currSlide);
            int slideCount = slides.Count;
            slides.Remove(currSlide);

            while (orderedSlides.Count < slideCount)
            {
                maxScore = 0;
                maxIndex = 0;
                

                for (int i = 0; i < slides.Count; i++)
                {
                    if(sn != i)
                    {
                        int score = currSlide.CalcScore(slides[i]);
                        if(score > maxScore)
                        {
                            maxScore = score;
                            maxIndex = i;
                        }
                    }
                }

                currSlide = slides[maxIndex];
                orderedSlides.Add(currSlide);
                slides.Remove(currSlide);
                sn = maxIndex;

            }


        }
        
        private void Output(string outFileName)
        {
            File.AppendAllText(outFileName, orderedSlides.Count() + Environment.NewLine);

            //Console.WriteLine(slides.Count());

            string photoOut = "";

            foreach (Slide s in orderedSlides)
            {
                
                foreach(Photo p in s.photos)
                {

                    photoOut += p.i + " ";
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
            photos = new List<Photo>();
        }

        public int CalcScore(Slide s)
        {
            int score = 0;

            int intersection = 0;
            int leftIntersection = 0;
            int rightIntersection = 0;

            List<string> leftTags = new List<string>(), rightTags = new List<string>();

            foreach(Photo p in this.photos)
            {
                foreach(string tag in p.tags)
                {
                    var exists = leftTags.Find(t => t.Equals(tag));

                    if (exists == null) leftTags.Add(tag);
                }
            }

            foreach (Photo p in s.photos)
            {
                foreach (string tag in p.tags)
                {
                    var exists = rightTags.Find(t => t.Equals(tag));

                    if (exists == null) rightTags.Add(tag);
                }
            }

            foreach(string lt in leftTags)
            {
                foreach(string rt in rightTags)
                {
                    if (lt.Equals(rt)) intersection++;
                    else leftIntersection++;
                }
            }

            foreach (string rt in rightTags)
            {
                foreach (string lt in leftTags)
                {
                    if (!rt.Equals(lt)) rightIntersection++;
                }
            }

            score = Math.Min(intersection, leftIntersection);
            score = Math.Min(score, rightIntersection);
            // get score between slide photos and new photo

            return score;
        }

        //public List<int> photos;
        public List<Photo> photos;
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
