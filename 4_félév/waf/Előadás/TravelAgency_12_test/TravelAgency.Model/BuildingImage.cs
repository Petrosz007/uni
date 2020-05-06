﻿using System;

namespace ELTE.TravelAgency.Model
{
	public class BuildingImage
	{
		public int Id { get; set; }
		public int BuildingId { get; set; }
		public byte[] ImageSmall { get; set; }
		public byte[] ImageLarge { get; set; }

		public Building Building { get; set; }
	}
}
