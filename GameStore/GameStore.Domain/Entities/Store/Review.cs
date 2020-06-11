using GameStore.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Store
{
    public class Review
    {
        public int Id { get; set; }
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Длина отзыва должна быть от 3 до 300 символов")]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Game Game { get; set; }
    }
}
