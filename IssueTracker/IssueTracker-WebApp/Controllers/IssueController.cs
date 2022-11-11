using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Services;
using IssueTracker_CoreServices.Models.DTO;

namespace IssueTracker_WebApp.Controllers
{
    public class IssueController : Controller
    {
        private readonly IServiceBase<ApplicationDbContext, Issue, int> _issues;
        private readonly IServiceBase<ApplicationDbContext, Project, int> _projects;

        public IssueController(IServiceBase<ApplicationDbContext, Issue, int> issues, IServiceBase<ApplicationDbContext, Project, int> projects)
        {
            _issues = issues;
            _projects = projects;
        }

        // GET: Issue
        public async Task<IActionResult> Index() => View(await _issues.GetAllAsync());

        public async Task<IActionResult> Project(int? id)
        {
            if (id == null)
                return NotFound();

            var project = await _projects.FindAsync(id.Value);
            if (project == null)
                return NotFound();

            project.Issues = (await _issues.GetAllAsync()).Where(x=>x.Project.Id== id.Value).ToList();

            return View(ProjectIssuesDto.CreateDto(project));
        }

        // GET: Issue/New
        public IActionResult New(int? id)
        {
            if (id == null)
                return NotFound();

            if (_projects.Exists(id.Value))
                return View(new IssueDto { ProjectId = id.Value });

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(int? id, [Bind("Id,Name,Description")] IssueDto dto)
        {
            Issue issue = null;
            if (ModelState.IsValid)
            {
                issue = IssueDto.ToBasicIssue(dto);
                issue.Created = new DateTime();

                var project = await _projects.FindAsync(id.Value);

                if(project == null)
                {
                    return NotFound();
                }
                ((List<Issue>)project.Issues).Add(issue);
                issue.Project = project;
                await _issues.AddAsync(issue);
                await _issues.SaveChangesAsync();
                await _projects.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _issues.FindAsync(id.Value);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issue/Create
        public IActionResult Create() => View();

        // POST: Issue/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,Created,ClosedDate")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                await _issues.AddAsync(issue);
                await _issues.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _issues.FindAsync(id.Value);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status,Created,ClosedDate")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _issues.Update(issue);
                    await _issues.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _issues.FindAsync(id.Value);

            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_issues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IssuesDb'  is null.");
            }
            var issue = await _issues.FindAsync(id);
            if (issue != null)
            {
                _issues.Remove(issue);
            }

            await _issues.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _issues.Exists(id);
        }
    }
}
