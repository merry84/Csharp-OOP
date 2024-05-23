﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class WeaponRepository :IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => weapons.AsReadOnly();

        public void AddItem(IWeapon model)
            => weapons.Add(model);

        public IWeapon FindByName(string name)
            => weapons.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveItem(string name)
            => weapons.Remove(weapons.FirstOrDefault(x => x.GetType().Name == name));
    }
}
