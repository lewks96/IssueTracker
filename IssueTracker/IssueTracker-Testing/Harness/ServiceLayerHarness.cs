using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_Testing.Harness
{
    public abstract class ServiceLayerTestHarness<TContext, TEntity, TComparable>
        where TContext : DbContext
        where TEntity : class, new()
        where TComparable : IComparable
    {
        protected TContext _context;
        protected DbSet<TEntity> _workingSet;
        protected IServiceBase<TEntity, TComparable> _sut;

        protected IEntitySpecialization<TEntity> _entitySpecialization;

        [OneTimeSetUp]
        public abstract void OneTimeSetup();

        protected abstract int GetPerTestRandomSeed();


        [Test]
        public void FindAsync_GetsElement_FromContextAdd()
        {
            var randomSeed = GetPerTestRandomSeed();
            var elem = EntityFactory.GenerateNew<TEntity>(new Random(randomSeed));

            _context.Add(elem);
            _context.SaveChanges();

            var result = _sut.FindAsync((TComparable)EntityFactory.GetRandomValueForType(new Random(randomSeed), typeof(TComparable))).Result;
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<TEntity>());
            Assert.That(result, Is.InstanceOf<TEntity>());

            _context.Remove(elem);
            _context.SaveChanges();
        }

        [Test]
        public void AddAsync_AddsElement_ContextChecked()
        {
            var randomSeed = GetPerTestRandomSeed();
            var elem = EntityFactory.GenerateNew<TEntity>(new Random(randomSeed));

            _sut.AddAsync(elem).Wait();
            _sut.SaveChangesAsync().Wait();

            var result = _context.FindAsync<TEntity>((TComparable)EntityFactory.GetRandomValueForType(new Random(randomSeed), typeof(TComparable))).Result;
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<TEntity>());
            Assert.That(result, Is.InstanceOf<TEntity>());

            _sut.Remove(elem);
            _sut.SaveChangesAsync().Wait();
        }

        [Test]
        public void GetAllAsync_ReturnsExpected_ContextChecked()
        {
            var randomSeed = GetPerTestRandomSeed();
            var random = new Random(randomSeed);

            var elems = new List<TEntity>()
            {
                EntityFactory.GenerateNew<TEntity>(new Random(random.Next())),
                EntityFactory.GenerateNew<TEntity>(new Random(random.Next())),
                EntityFactory.GenerateNew<TEntity>(new Random(random.Next())),
                EntityFactory.GenerateNew<TEntity>(new Random(random.Next())),
            };

            _context.AddRange(elems);
            _context.SaveChangesAsync().Wait();

            List<TEntity> result = (List<TEntity>)_sut.GetAllAsync().Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result[0], Is.TypeOf<TEntity>());
            Assert.That(result[0], Is.InstanceOf<TEntity>());
            Assert.That(result[1], Is.TypeOf<TEntity>());
            Assert.That(result[1], Is.InstanceOf<TEntity>());

            _context.RemoveRange(elems);
            _context.SaveChangesAsync().Wait();
        }

        [Test]
        public void Exists_Expected_FromContextAdd()
        {
            var randomSeed = GetPerTestRandomSeed();
            var elem = EntityFactory.GenerateNew<TEntity>(new Random(randomSeed));

            _context.Add(elem);
            _context.SaveChanges();

            var result = _sut.Exists((TComparable)EntityFactory.GetRandomValueForType(new Random(randomSeed), typeof(TComparable)));
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(true));

            _context.Remove(elem);
            _context.SaveChanges();
        }


        [Test]
        public void Update_Does_Expected()
        {
            var randomSeed = GetPerTestRandomSeed();
            var elem = EntityFactory.GenerateNew<TEntity>(new Random(randomSeed));

            _context.Add(elem);
            _context.SaveChanges();

            var findResult = _context.FindAsync<TEntity>((TComparable)EntityFactory.GetRandomValueForType(new Random(randomSeed), typeof(TComparable))).Result;
            Assert.That(findResult, Is.Not.Null);
            _entitySpecialization.ModifyEntity(findResult!);

            _sut.Update(findResult!);
            var result = _context.FindAsync<TEntity>((TComparable)EntityFactory.GetRandomValueForType(new Random(randomSeed), typeof(TComparable))).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(_entitySpecialization.CompareEntity(result, _entitySpecialization.ModifyEntity(elem)), Is.True);

            _context.Remove(elem);
            _context.SaveChanges();
        }
    }

}
