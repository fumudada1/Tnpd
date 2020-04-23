using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class Holiday : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "是否假日")]
        public bool IsHoliday { get; set; }

        [Display(Name = "假日類別")]
        public string HolidayCategory { get; set; }

        [Display(Name = "假日說明")]
        public string Description { get; set; }


        #region "計算跳過假日後幾天是哪一天"

        public DateTime GetWorkDay(DateTime startDate,int num)
        {
            BackendContext db = new BackendContext();

            DateTime enDate = new DateTime();
            int i = 1;
            int j = 1;
            var holidays = db.Holidays.ToList();
            while (i <= num)
            {
                enDate = startDate.AddDays(j);
                var day = holidays.FirstOrDefault(x => x.InitDate == enDate);
                if (day == null)
                {
                    i++;
                }
                else
                {
                    if (day.IsHoliday == false)
                    {
                        i++;
                    }
                }

                j++;
            }
            return enDate;
        }
        #endregion


    }
}
