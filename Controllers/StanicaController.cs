using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebProjekat3031.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class StanicaController:ControllerBase{
        public SContext Context { get; set; }
        public StanicaController(SContext context){
            Context=context;
        }

        [HttpGet]
        [Route("Stanica")]
        public async Task<ActionResult> Stanice(){
            try{
                var stanice=await Context.Stanice.ToListAsync();
                return Ok(stanice);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Putovanje")]
        public async Task<ActionResult> PutnikPutovanje(string m,string i, string p,int tID,int dID,string tS){
            try
            {
                Putnik putnik=await Context.Putnici.Where(p=>p.eMail==m).FirstOrDefaultAsync();
                if(putnik==null){
                    putnik=new Putnik{
                        Ime=i,
                        eMail=m,
                        Prezime=p
                    };
                    Context.Add(putnik);
                }
                Termin termin=await Context.Termini.Where(t=>t.ID==tID).FirstOrDefaultAsync();
                Destinacija destinacija=await Context.Destinacije.Where(d=>d.ID==dID).FirstOrDefaultAsync();
                int cena;
                if(tS=="AC1"){
                    cena=2500;
                }
                else if(tS=="AC2"){
                    cena=1200;
                }
                else
                    cena=650;
                Putovanje putovanje=new Putovanje{
                    Putnik=putnik,
                    Destinacija=destinacija,
                    Vreme=termin.Vreme,
                    tipSedista=tS,
                    Cena=cena
                };
                Context.Putovanja.Add(putovanje);
                await Context.SaveChangesAsync();
               return Ok(putovanje); 
            }
            catch (Exception e)
            {
                
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("Putovanja")]
        public async Task<ActionResult> UcitajSvaPutovanjaZaJednogPutnikaiStanicu(string eMail,int stanicaID){
            try{
                var putnik=await Context.Putnici.Where(p=>p.eMail==eMail).FirstOrDefaultAsync();
                if(putnik==null)
                    return BadRequest("Nepostojeci putnik....");
                var stanica=await Context.Stanice.Where(st=>st.ID==stanicaID).FirstOrDefaultAsync();
                if(stanica==null)
                    return BadRequest("Stanice.......nema....muka...teska...");
                var putovanja=await Context.Putovanja.Where(put=>put.Putnik==putnik&&put.Destinacija.Stanica==stanica).ToListAsync();
                if(putovanja.Count<1)
                    return BadRequest("Putnik ne putuje!");
                return Ok(putovanja);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("Sediste")]
        public async Task<ActionResult> IzmeniTipSedista(int putovanjeID,string tipSedista){
            try{
                var putovanje=await Context.Putovanja.Where(p=>p.ID==putovanjeID).FirstOrDefaultAsync();
                if(putovanje==null)
                    return BadRequest("Los ti je ID, druze...");
                putovanje.tipSedista=tipSedista;
                int cena;
                if(tipSedista=="AC1"){
                    cena=2500;
                }
                else if(tipSedista=="AC2"){
                    cena=1200;
                }
                else
                    cena=650;
                putovanje.tipSedista=tipSedista;
                putovanje.Cena=cena;
                await Context.SaveChangesAsync();
                return Ok("Bez greske!");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("DodajPutnika")]
        public async Task<ActionResult> DodajPutnika(string email,string ime,string prezime){
            try{
                Putnik putnik=Context.Putnici.Where(p=>p.eMail==email).FirstOrDefault();
                if(putnik==null){
                    putnik=new Putnik{
                        Ime=ime,
                        Prezime=prezime,
                        eMail=email
                    };
                    Context.Putnici.Add(putnik);
                    await Context.SaveChangesAsync();
                    return Ok("Uspeshno dodat pooootnik! Id je " +putnik.ID );
                }
                return BadRequest("Putnik vec postoji, druuuuze...");
            }
            catch(Exception e){
                return BadRequest(e.Message+("brt nesto si pogresio"));

            }
        }

        [HttpPost]
        [Route("Destinacija")]
        public async Task<ActionResult> DodajDestinaciju(string grad,int idStanice){
            try{
                Stanica stanica=await Context.Stanice.Where(s=>s.ID==idStanice).FirstOrDefaultAsync();
                if(stanica==null)
                    return BadRequest("Nepostojeca stanica, brt....");
                Destinacija destinacija=await Context.Destinacije.Where(d=>d.Grad==grad).FirstOrDefaultAsync();
                if(destinacija==null){
                    destinacija=new Destinacija{
                        Grad=grad,
                        Termini=null,
                        Putovanja=null,
                        Stanica=stanica
                    };
                }
                Context.Destinacije.Add(destinacija);
                await Context.SaveChangesAsync();
                return Ok("Mjau mjau "+destinacija.Grad+" je dodat!");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("VremeUDestinaciju")]
        public async Task<ActionResult> DodajVremeUDestinaciju(string tvreme, int destinationID){
            try
            {
                Destinacija destinacija=await Context.Destinacije
                .Where(d=>d.ID==destinationID)
                .FirstOrDefaultAsync();
                if(destinacija==null)
                    return BadRequest("Nepostojeca destinacija, druze!");
                DateTime curr=DateTimeOffset.Parse(tvreme).UtcDateTime;
                Termin termin = await Context.Termini
                .Where(t=>t.Vreme==curr&t.Destinacija==destinacija)
                .FirstOrDefaultAsync();
                if(termin==null){
                    termin=new Termin{
                        Vreme=curr,
                        Destinacija=destinacija
                    };
                    Context.Termini.Add(termin);
                    await Context.SaveChangesAsync();
                    return Ok("Uspeshno dodat termin!!! "+ termin.Vreme);
                }
                return BadRequest("Nece radi bato, na mac bato");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePutnika")]
        public async Task<ActionResult> ObrisiPutnika(string email, int stanicaID){
            try
            {
                Putnik putnik = await Context.Putnici.Where(dp=>dp.eMail==email).FirstOrDefaultAsync();
                if (putnik == null){
                    return BadRequest("Putnik ne postoji!");
                }
                Stanica stanica=await Context.Stanice.Where(xD=>xD.ID==stanicaID).FirstOrDefaultAsync();
                if(stanica==null)
                    return BadRequest("Nepostojeca stanica@!@@");
                var putovanja=await Context.Putovanja
                                            .Where(p=>p.Putnik==putnik&&p.Destinacija.Stanica==stanica)
                                            .ToListAsync();
                if(putovanja.Count<1)
                    return BadRequest("Ovaj momak uoooooopste ne putuje....");
                return Ok("Ubili ste snove jednog putnika....");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("Destinacije")]
        public async Task<ActionResult> UcitajDestinacijeZaStanicu(int stanicaID){
            try{
                var destinacije=await Context.Destinacije.Where(put=>put.Stanica.ID==stanicaID).Select(p=>new{
                    ID=p.ID,
                    Grad=p.Grad
                }).ToListAsync();
                if(destinacije.Count<1)
                    return BadRequest("Putnik ne putuje!");
                return Ok(destinacije);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("Termini")]
        public async Task<ActionResult> UcitajTermineZaDestinaciju(int destinacijaID){
            try{
                var termini=await Context.Termini.Where(p=>p.Destinacija.ID==destinacijaID).Select(p=>new{
                    id=p.ID,
                    vreme=p.Vreme.ToString()
                }).ToListAsync();
                if(termini==null)
                    return BadRequest("Nepostojeca destinacija..........");
                return Ok(termini);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmenaMejla")]

        public async Task<ActionResult> IzmenaMejla(string stari, string novi){
            try{
                var putnik=await Context.Putnici.Where(p=>p.eMail==stari).FirstOrDefaultAsync();
                if(putnik==null)
                    return BadRequest("Putnik sa mail-om:"+stari+" ne postoji!");
                putnik.eMail=novi;
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("Brisi")]
        public async Task<ActionResult> BrisiSvaPutovanjaSaTipomSedista(string email,string tS){
            try{
                Putnik putnik=await Context.Putnici.Where(p=>p.eMail==email).FirstOrDefaultAsync();
                if(putnik==null)
                    return BadRequest("Nepostojeci putnik!");
                var putovanja = await Context.Putovanja.Where(p=>p.Putnik==putnik && p.tipSedista==tS).ToListAsync();
                if(putovanja.Count<1)
                    return BadRequest("Putnik nema rezervisano nijedno putovanje tipa: "+tS);
                putovanja.ForEach(putovanje=>{
                    Context.Remove(putovanje);
                });
                await Context.SaveChangesAsync();
                return Ok("Uspeh!");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}