using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Core
{

    public class Rootobject
    {
        public Pagination pagination { get; set; }
        public Datum[] data { get; set; }
        public Meta meta { get; set; }
    }

    public class Pagination
    {
        public string next_max_id { get; set; }
        public string next_url { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public User user { get; set; }
        public Images images { get; set; }
        public string created_time { get; set; }
        public Caption caption { get; set; }
        public bool user_has_liked { get; set; }
        public Likes likes { get; set; }
        public string[] tags { get; set; }
        public string filter { get; set; }
        public Comments comments { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public Location location { get; set; }
        public object attribution { get; set; }
        public Users_In_Photo[] users_in_photo { get; set; }
        public Carousel_Media[] carousel_media { get; set; }
        public Videos videos { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Images
    {
        public Thumbnail thumbnail { get; set; }
        public Low_Resolution low_resolution { get; set; }
        public Standard_Resolution standard_resolution { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Standard_Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Caption
    {
        public string id { get; set; }
        public string text { get; set; }
        public string created_time { get; set; }
        public From from { get; set; }
    }

    public class From
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
    }

    public class Location
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public long id { get; set; }
    }

    public class Videos
    {
        public Standard_Resolution1 standard_resolution { get; set; }
        public Low_Resolution1 low_resolution { get; set; }
        public Low_Bandwidth low_bandwidth { get; set; }
    }

    public class Standard_Resolution1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

    public class Low_Resolution1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

    public class Low_Bandwidth
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

    public class Users_In_Photo
    {
        public User1 user { get; set; }
        public Position position { get; set; }
    }

    public class User1
    {
        public string username { get; set; }
    }

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Carousel_Media
    {
        public Images1 images { get; set; }
        public Users_In_Photo1[] users_in_photo { get; set; }
        public string type { get; set; }
    }

    public class Images1
    {
        public Thumbnail1 thumbnail { get; set; }
        public Low_Resolution2 low_resolution { get; set; }
        public Standard_Resolution2 standard_resolution { get; set; }
    }

    public class Thumbnail1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Resolution2
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Standard_Resolution2
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Users_In_Photo1
    {
        public User2 user { get; set; }
        public Position1 position { get; set; }
    }

    public class User2
    {
        public string username { get; set; }
    }

    public class Position1
    {
        public float x { get; set; }
        public float y { get; set; }
    }

}
