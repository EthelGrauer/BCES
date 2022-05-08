using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public enum Party
    {
        Avoda=1,
        YahadutHatora,
        Yamina,
        HareshimaHameshutefet,
        KacholLavan,
        IsraelBeitenu,
        HaLikud,
        Meretz,
        Shas,
        TikvaHadasha,
        HatzionutHadatit,
        PetekLavan
        
    }
    public class Party_ins
    {
        public string name { get; set; }
        public string link { get; set; }
        public string info { get; set; }

        public Party_ins(int choice)
        {
            
            switch (choice)
            {
                case (int)Party.Avoda:
                    this.name ="העבודה";
                     this.link = "https://www.labor.org.il/havoda/knesset-24-elections/23841-havoda-platform-24th-knesset.html";
                     this.info =" העבודה בראשות מירב מיכאלי משויכת לשמאל הפוליטי בישראל. המפלגה הוקמה ב-1968 ואיחדה את מפלגת השלטון מפאי ומפלגות קטנות יותר. גולדה מאיר, יצחק רבין ואהוד ברק היו מראשי העבודה";
                    break;
                //case (int)Party.YahadutHatora:
                //     this.name ="יהדות התורה";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.Yamina:
                //     this.name ="ימינה";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.HareshimaHameshutefet:
                //     this.name ="הרשימה המשותפת";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.KacholLavan:
                //     this.name ="כחול לבן";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.PetekLavan:
                //     this.name ="פתק לבן";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.TikvaHadasha:
                //     this.name ="תקווה חדשה";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.IsraelBeitenu:
                //     this.name ="ישראל ביתנו";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.Meretz:
                //     this.name ="מרץ";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.HatzionutHadatit:
                //     this.name ="הציונות הדתית";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.Shas:
                //     this.name ="שס";
                //     this.link =;
                //     this.info =;
                //    break;
                //case (int)Party.HaLikud:
                //     this.name ="הליכוד";
                //     this.link =;
                //    this.info =;
                //    break;
            }
            
        }
    }
}
