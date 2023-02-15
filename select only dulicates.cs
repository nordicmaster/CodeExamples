var duplicates_list = list.GroupBy(x => x._desired_unique_parameter).Where(g => g.Count() > 1).SelectMany(x => x).ToList();
