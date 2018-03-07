﻿using hase.DevLib.Contract;
using System;
using System.Collections.Generic;

namespace hase.DevLib.Client
{
    public static class ClientUtil
    {
        public static IEnumerable<(int Id, string Desc)> GetServices()
        {
            var serviceTypesEnum = typeof(ServiceTypesEnum);
            foreach (var vlu in Enum.GetValues(serviceTypesEnum))
            {
                var id = Convert.ToInt32(vlu);
                var desc = Enum.GetName(serviceTypesEnum, id);
                yield return (id, desc);
            }
        }

    }
}

