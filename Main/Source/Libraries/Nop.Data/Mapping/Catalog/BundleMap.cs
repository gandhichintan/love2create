﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class BundleMap : EntityTypeConfiguration<Bundle>
    {
        public BundleMap()
        {
            this.ToTable("Bundle");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(400);
            this.Property(p => p.ShortDescription);
            this.Property(p => p.FullDescription);
        }
    }
}