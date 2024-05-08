﻿using softcad_web_api.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOperacaoCuriosidade.Domain.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set;}
        public string Email { get; set;}
        public string Address { get; set;}
        public string? Information { get; set;}
        public string Interests { get; set;}
        public string Feelings { get; set;}
        public string Principles { get; set;}
        public bool Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastModification { get; set; }
        public int AdministratorId { get; set; }

    }
}
