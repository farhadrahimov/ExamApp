﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.DTO
{
    public class StudentPostModel
    {
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}