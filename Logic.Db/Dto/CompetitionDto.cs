﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class CompetitionDto {
        public long ID;
        public string Name;
        public TypeCompetition Type;
        public double Km;
        public double Price;
        public DateTime Date;
        public int NumberPlaces;
        public string Status; //can be null
        public byte[] Rules;
        public double Slope;
        public int Milestone;
    }

    public enum TypeCompetition {
        Mountain = 'M', Asphalt = 'S'
    }
}
