using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFriends.Models
{
    public class Friend
    {
        public Friend()
        {
            Images = new List<Image>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "שם פרטי")] // תווית לשדה בתצוגה
        public string FirstName { get; set; } = "";

        [Display(Name = "שם משפחה")]
        public string? LastName { get; set; }

        [Display(Name = "שם מלא"), NotMapped] // שדה שאינו נשמר בטבלה
        public string FullName { get { if (LastName != null) return FirstName + " " + LastName; return FirstName; } }

        [EmailAddress(ErrorMessage = "שדה אינו תקין - אימייל נדרש")]
        public string? Email { get; set; }
        [Phone, Display(Name = "מספר טלפון")]
        public string Phone { get; set; }

        public List<Image> Images { get; set; }



        [Display(Name = "הוספת תמונה"), NotMapped]
        public IFormFile? SetImage
        {
            get { return null; }
            set
            {
                if (value != null)
                {
                    AddImage(value);
                }
            }
        }

        public void AddImage(IFormFile file) // קובץ המכיל תמונה מטופס html
        {
            if (file == null) return;

            // יצירת מקום בזיכרון במכיל קובץ
            MemoryStream stream = new MemoryStream();
            // העתקת הקובץ למקום בזיכרון
            file.CopyTo(stream);
            // הפיכת המיקום בזיכרון לבייטים ושליחתם לפונקיצה הבאה
            AddImage(stream.ToArray());

        }

        public void AddImage(byte[] myImage)
        {
            // יצירת אובייקט חדש מסוג תמונה
            Image image = new()
            {
                Friend = this,
                MyImage = myImage
            };

            Images.Add(image);
        }
    }
}
