using System;
using System.Collections.Generic;

namespace Domain.Entities;

    public class ClassModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsAbstract { get; set; }
        public Guid? BaseClassId { get; set; }
        public bool IsSealed { get; set; }

        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();
        public List<MethodModel> Methods { get; set; } = new List<MethodModel>();
    }
