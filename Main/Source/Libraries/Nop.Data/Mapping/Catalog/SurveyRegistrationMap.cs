﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Catalog;


namespace Nop.Data.Mapping.Catalog
{
    public partial class SurveyRegistrationMap : EntityTypeConfiguration<SurveyRegistration>
    {
        public SurveyRegistrationMap()
        {
            this.ToTable("SurveyRegistration");
            this.HasKey(c => c.Id);
        }
    }
}
