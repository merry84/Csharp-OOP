﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;

namespace Heroes.Repositories
{
    public class WeaponRepository :IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models =>weapons.AsReadOnly();

        public void Add(IWeapon model)
            => weapons.Add(model);

        public bool Remove(IWeapon model)
            => weapons.Remove(model);

        public IWeapon FindByName(string name)
            => weapons.FirstOrDefault(x => x.Name == name);
    }
}
