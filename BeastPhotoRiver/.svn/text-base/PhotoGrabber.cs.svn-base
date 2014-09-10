using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BeastPhotoRiver
{
    class PhotoGrabber
    {
        //private String _home_directory;
        //private String[] _folders_in_home_directory;
        private List<String> _big_photos;
        //private String[] _text_array;


        public PhotoGrabber(String directory)
        {
            //this._home_directory = directory;
            _big_photos = new List<String>();

            try
            {
                String[] _photos_array = Directory.GetFiles(directory, "*.jpg");
                foreach (String s in _photos_array)
                {
                    //if (!s.Contains("small"))
                    //{
                        _big_photos.Add(s);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        /// <summary>
        /// C. Valdes
        /// Returns a list of Strings that contains the paths to all the large .jpg files
        /// </summary>
        /// <returns>List of String paths to jpg images from all directories</returns>
        public String[] GetBigPhotos()
        {
            return _big_photos.ToArray();
        }

        /// <summary>
        /// C. Valdes
        /// From the String path of a big photo, grab the text inside the image's -info.txt file
        /// </summary>
        /// <param name="photoPath">Path for large jpg</param>
        /// <returns>String with following info: [0]-title; [1]-location; [2]-story</returns>
        /*public String[] GetPhotoInfoFromBigPhoto(String photoPath)
        {
            string pathToInfo = photoPath.Replace(".jpg", "-info.txt");
            pathToInfo = pathToInfo.Replace(".JPG", "-info.txt");
            StreamReader streamReader = new StreamReader(pathToInfo);
            String temp1 = streamReader.ReadLine();
            String temp2 = streamReader.ReadLine();
            String temp3 = streamReader.ReadToEnd();
            String[] newOne = { temp1, temp2, temp3 };
            return newOne;          
        }

        /// <summary>
        /// C. Valdes
        /// From large jpg path, get the small image path
        /// </summary>
        /// <param name="photoPath">string path to large jpg</param>
        /// <returns>string path to small image jpg</returns>
        public String GetSmallPhotoFromBigPhoto(String photoPath)
        {
            String pathToSmallImage = photoPath.Replace(".jpg", "-small.jpg");
            if (Directory.Exists(pathToSmallImage))
                return pathToSmallImage;
            else
                return "does not exist";
        }

        /// <summary>
        /// C. Valdes
        /// From large image string path, get the author's info from the personal-info.txt file and return
        /// </summary>
        /// <param name="photoPath">string large photo path</param>
        /// <returns>String of author's info from text file: FirstName\n\nLastName\nPosition, Class of 20** </returns>
        public String GetAuthorInfoFromBigPhoto(String photoPath)
        {
            DirectoryInfo parentInfo = Directory.GetParent(photoPath);
            FileInfo[] filesInParent = parentInfo.GetFiles("personal-info.txt");
            String pathFinally = filesInParent[0].FullName;
            StreamReader streamReader = new StreamReader(pathFinally);
            String text = streamReader.ReadToEnd();
            streamReader.Close();
            String returnCharsInString = "\r\n";
            int index = text.IndexOf(returnCharsInString);
            String toReturn = "";
            //first name
            index = text.IndexOf(returnCharsInString);
            toReturn = toReturn + text.Substring(0, index);
            text = text.Substring(index + returnCharsInString.Length*2);
            
            //last name
            index = text.IndexOf(returnCharsInString);
            toReturn = toReturn + " " + text.Substring(0, index);
            text = text.Substring(index + returnCharsInString.Length);

            //position & class
            index = text.IndexOf(returnCharsInString);
            toReturn = toReturn + "\n" + text;
            //Console.WriteLine("index now..." + index);
            return toReturn;
        }
        */
    }
}
