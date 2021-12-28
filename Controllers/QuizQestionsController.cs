#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eqirl.Data;
using Eqirl.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text;
//using Google.Cloud.Translation.V2;

namespace Eqirl.Controllers
{
    public class QuizQestionsController : Controller
    {
        private readonly EqirlContext _context;
       
        private static readonly string subscriptionKey = "3281e1625ad340c4886c736f9858be39";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "centralindia";



        public QuizQestionsController(EqirlContext context)
        {
            _context = context;
        }

        private static readonly HttpClient client = new HttpClient
        {
            DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", subscriptionKey } }
        };

        // GET: QuizQestions
        [Authorize]
  
        public async Task<IActionResult> Index()
        {
          
            var eqirlContext = _context.QuizQestions.Include(q => q.Exam);
            return View(await eqirlContext.ToListAsync());
        }

        [Authorize]

        public async Task<IActionResult> Index1() 
        {
            var cruddbdemoContext = _context.QuizQestions.Include(e => e.Exam);
            return View(await cruddbdemoContext.ToListAsync());
        }
      
        public static async Task<string> Translate(string text,string codefrom,string codeto)
        {


            /* 
      var encodedText=WebUtility.UrlEncode("hi");
      var uri = "https://api.cognitive.microsofttranslator.com?" + $"to={text}&text={encodedText}";
      var result1 = await client.GetStringAsync(uri);
     */

            string route = "/translate?api-version=3.0&"+$"from={codefrom}&"+$"to={codeto}";
            string textToTranslate = text;
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);


            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                Console.WriteLine($"{requestBody}");
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);



                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result1 = await response.Content.ReadAsStringAsync();
                // return XElement.Parse(result).Value; 
                dynamic deserializedOutput = JsonConvert.DeserializeObject(result1);


                // Console.WriteLine(deserializedOutput[0]["translations"][0]["text"]);

                return deserializedOutput[0]["translations"][0]["text"];

            }
            


        }


        public async Task<IActionResult> result(int val)
        {
           QuizQestion quizQestion = new QuizQestion();
            quizQestion.FKDeptID = val;
            quizQestion.Question = "h";
            quizQestion.Option1 = "h";
            quizQestion.Option2 = "h";
            quizQestion.Option3 = "h";
            quizQestion.Option4 = "h";
            quizQestion.CorrectAnswer = 1;
            quizQestion.Answer = 1;

            ViewData["Message"] = quizQestion;
            

            var eqirlContext = _context.QuizQestions.Include(e => e.Exam);
           
            return View(await eqirlContext.ToListAsync());
        }

        // GET: QuizQestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizQestion = await _context.QuizQestions
                .Include(q => q.Exam)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quizQestion == null)
            {
                return NotFound();
            }

            return View(quizQestion);
        }

        // GET: QuizQestions/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ExamName");
            return View();
        }

        // POST: QuizQestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FKDeptID,Question,Option1,Option2,Option3,Option4,CorrectAnswer,Answer")] QuizQestion quizQestion)
        {
   





            if (!ModelState.IsValid)
            {




                if (quizQestion.FKDeptID == 1)
                {
                    

                    quizQestion.Question = await Translate(quizQestion.Question, "en","mr");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "mr");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "mr");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "mr");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "mr");


                    Console.WriteLine("data coming");

                    _context.Add(quizQestion);
                }

                else if (quizQestion.FKDeptID == 2)
                {
                    

                    quizQestion.Question = await Translate(quizQestion.Question, "en", "hi");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "hi");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "hi");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "hi");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "hi");



                    Console.WriteLine("data coming");

                    _context.Add(quizQestion);
                }

                else if (quizQestion.FKDeptID == 3)
                {
                  

                    quizQestion.Question = await Translate(quizQestion.Question, "en", "ta");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "ta");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "ta");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "ta");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "ta");



                    Console.WriteLine("data coming");

                    _context.Add(quizQestion);
                }



                else if (quizQestion.FKDeptID == 4)
                {
                   
                    quizQestion.Question = await Translate(quizQestion.Question, "en", "gu");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "gu");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "gu");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "gu");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "gu");



                    Console.WriteLine("data coming");

                    _context.Add(quizQestion);
                }

                else if (quizQestion.FKDeptID == 5)
                {
                    
                    quizQestion.Question = await Translate(quizQestion.Question, "en", "en");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "en");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "en");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "en");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "en");



                    Console.WriteLine("data coming");

                    _context.Add(quizQestion);
                }
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ID", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        // GET: QuizQestions/Edit/5


        public async Task<IActionResult> lang(int? id)
        {
          

            var quizQestion = await _context.QuizQestions.FindAsync(id);
            if (quizQestion == null)
            {
                return NotFound();
            }
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ExamName", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        // POST: QuizQestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> lang(int id,int fr, [Bind("ID,FKDeptID,Question,Option1,Option2,Option3,Option4,CorrectAnswer,Answer")] QuizQestion quizQestion)
        {
            var to = "";
            var from = "";
            if (id == 1) from = "mr";
            else if (id == 2) from = "hi";
            else if (id == 3) from = "ta";
            else if (id == 4) from = "gu";
            else if (id == 5) from = "en";

            if (quizQestion.FKDeptID == 1) to = "mr";
            else if (quizQestion.FKDeptID == 2) to = "hi";
            else if (quizQestion.FKDeptID == 3) to = "ta";
            else if (quizQestion.FKDeptID == 4) to = "gu";
            else if (quizQestion.FKDeptID == 5) to = "en";
          

            if (!ModelState.IsValid)
            {
                try
                {

                    quizQestion.Question = await Translate(quizQestion.Question, $"{from}", $"{to}");
                    quizQestion.Option1 = await Translate(quizQestion.Option1, $"{from}", $"{to}");
                    quizQestion.Option2 = await Translate(quizQestion.Option2, $"{from}", $"{to}");
                    quizQestion.Option3 = await Translate(quizQestion.Option3, $"{from}", $"{to}");
                    quizQestion.Option4 = await Translate(quizQestion.Option4, $"{from}", $"{to}");


                    Console.WriteLine("data coming");

                    _context.Update(quizQestion);
                 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizQestionExists(quizQestion.ID))
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
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ID", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizQestion = await _context.QuizQestions.FindAsync(id);
            if (quizQestion == null)
            {
                return NotFound();
            }
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ExamName", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        // POST: QuizQestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FKDeptID,Question,Option1,Option2,Option3,Option4,CorrectAnswer,Answer")] QuizQestion quizQestion)
        {
            if (id != quizQestion.ID)
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                try
                {


                    if (quizQestion.FKDeptID == 1)
                    {

                        quizQestion.Question = await Translate(quizQestion.Question, "en", "mr");
                        quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "mr");
                        quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "mr");
                        quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "mr");
                        quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "mr");


                        Console.WriteLine("data coming");

                        _context.Update(quizQestion);
                    }

                    else if (quizQestion.FKDeptID == 2)
                    {

                        quizQestion.Question = await Translate(quizQestion.Question, "en", "hi");
                        quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "hi");
                        quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "hi");
                        quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "hi");
                        quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "hi");



                        Console.WriteLine("data coming");

                        _context.Update(quizQestion);
                    }

                    else if (quizQestion.FKDeptID == 3)
                    {

                        quizQestion.Question = await Translate(quizQestion.Question, "en", "ta");
                        quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "ta");
                        quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "ta");
                        quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "ta");
                        quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "ta");



                        Console.WriteLine("data coming");

                        _context.Update(quizQestion);
                    }



                    else if (quizQestion.FKDeptID == 4)
                    {

                        quizQestion.Question = await Translate(quizQestion.Question, "en", "gu");
                        quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "gu");
                        quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "gu");
                        quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "gu");
                        quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "gu");



                        Console.WriteLine("data coming");

                        _context.Update(quizQestion);
                    }

                    else if (quizQestion.FKDeptID == 5)
                    {

                        quizQestion.Question = await Translate(quizQestion.Question, "en", "en");
                        quizQestion.Option1 = await Translate(quizQestion.Option1, "en", "en");
                        quizQestion.Option2 = await Translate(quizQestion.Option2, "en", "en");
                        quizQestion.Option3 = await Translate(quizQestion.Option3, "en", "en");
                        quizQestion.Option4 = await Translate(quizQestion.Option4, "en", "en");



                        Console.WriteLine("data coming");

                        _context.Update(quizQestion);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizQestionExists(quizQestion.ID))
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
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ID", quizQestion.FKDeptID);
            return View(quizQestion);
        }


        public async Task<IActionResult> AnsUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizQestion = await _context.QuizQestions.FindAsync(id);
            if (quizQestion == null)
            {
                return NotFound();
            }
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ExamName", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        // POST: QuizQestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnsUpdate(int id, [Bind("ID,FKDeptID,Question,Option1,Option2,Option3,Option4,CorrectAnswer,Answer")] QuizQestion quizQestion)
        {
            if (id != quizQestion.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizQestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizQestionExists(quizQestion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index1));
            }
            ViewData["FKDeptID"] = new SelectList(_context.Set<Exam>(), "ID", "ID", quizQestion.FKDeptID);
            return View(quizQestion);
        }

        // GET: QuizQestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizQestion = await _context.QuizQestions
                .Include(q => q.Exam)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quizQestion == null)
            {
                return NotFound();
            }

            return View(quizQestion);
        }




        // GET: QuizQestions/Delete/5



        // POST: QuizQestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizQestion = await _context.QuizQestions.FindAsync(id);
            _context.QuizQestions.Remove(quizQestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizQestionExists(int id)
        {
            return _context.QuizQestions.Any(e => e.ID == id);
        }
    }
}
