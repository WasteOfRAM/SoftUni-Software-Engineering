using System;

namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            var file = new File("afaf", 4, 5);
            var music = new Music("ASf", "asfa", 4, 2);

            var streamFile = new StreamProgressInfo(file);
            var streamMusic = new StreamProgressInfo(music);
            
        }
    }
}
