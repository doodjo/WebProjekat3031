import {Destinacija} from "./Destinacija.js";
import { Termini } from "./Termin.js";
export class Stanica{
    constructor(id,ime){
        this.id=id;
        this.ime=ime;
        this.kont=null;
    }
    crtaj(){
        this.kont=document.createElement("div");
        this.kont.className="stanicaKontejner"+this.id;
        document.body.appendChild(this.kont);

        this.crtajDodaj();
        this.crtajMenjaj();
        this.crtajBrisi();
    }
    crtajDodaj(){
        let dodajContainer=document.createElement("div");
        dodajContainer.className="dodaj";
        this.kont.appendChild(dodajContainer);
        this.crtajInput(dodajContainer,"E-Mail","text","mejl");
        this.crtajInput(dodajContainer,"Ime","text","ime");
        this.crtajInput(dodajContainer,"Prezime","text","prezime");

        let list=["AC1","AC2","AC3"];
        let xD=document.createElement("select");
        xD.className="tipSedista";
        dodajContainer.appendChild(xD);
        let o;
        list.forEach(l=>{
            o=document.createElement("option");
            o.innerHTML=l;
            xD.appendChild(o);
        });

        let select=document.createElement("select");
        select.className="destinacije";
        dodajContainer.appendChild(select);
        select.onchange=(ev)=>{
            let terminSelect=dodajContainer.querySelector(".termin");
            let options=terminSelect.querySelectorAll("option");
            if(options.length<0)
                return;
            options.forEach(option=>{
                terminSelect.removeChild(option);
            });
            let destinacijaID=select.options[select.selectedIndex].value;
            fetch("https://localhost:5001/Stanica/Termini?destinacijaID="+destinacijaID,{
                method: "GET"
            })
            .then(response=>{
                response.json().then(termini=>{
                    let op;
                    termini.forEach(termin=>{
                        op=document.createElement("option");
                        op.innerHTML=termin.vreme;
                        op.value=termin.id;
                        terminSelect.appendChild(op);
                    });
                    this.ucitajTermine2();
                });
            });

        }
        this.ucitajDestinacije(select);

    }
    crtajMenjaj(){
        let menjajContainer=document.createElement("div");
        menjajContainer.className="menjaj";
        this.kont.appendChild(menjajContainer);
        this.crtajInput(menjajContainer,"Trenutni E-Mail","text","mejl1");
        this.crtajInput(menjajContainer,"Novi E-Mail","text","mejl2");
        let button=document.createElement("button");
        button.innerHTML="Izmeni!";
        menjajContainer.appendChild(button);
        button.onclick=(ev)=>{
            let cI1=menjajContainer.querySelector("input[name='mejl1']");
            let stari=cI1.value;
            if(stari===undefined||stari===""){
                alert("Unesite stari mejl!")
                return;
            }
            let cI2=menjajContainer.querySelector("input[name='mejl2']");
            let novi=cI2.value;
            if(novi===undefined||novi===""){
                alert("Unesite stari mejl!")
                return;
            }
            fetch("https://localhost:5001/Stanica/IzmenaMejla?stari="+stari+"&novi="+novi,{
                method: "PUt"
            })
            .then(msg=>{
                if(msg.ok){
                    alert("SVe je u redu!!!Putnikov novi mail "+novi);
                    return;
                }
                else{
                    alert("kme kme");
                    return;
                }
            })
        }
    }
    crtajBrisi(){
        let brisiContainer=document.createElement("div");
        brisiContainer.className="brisi";
        this.kont.appendChild(brisiContainer);
        this.crtajInput(brisiContainer,"E-Mail","text","mejl");
        let list=["AC1","AC2","AC3"];
        let xD=document.createElement("select");
        xD.className="tipSedista";
        brisiContainer.appendChild(xD);
        let o;
        list.forEach(l=>{
            o=document.createElement("option");
            o.innerHTML=l;
            xD.appendChild(o);
        });
        let button=document.createElement("button");
        button.innerHTML="Brisi!";
        brisiContainer.appendChild(button);
        button.onclick=(ev)=>{
            let email=brisiContainer.querySelector("input[name='mejl']").value;//email
            let select=brisiContainer.querySelector(".tipSedista");
            let tS=select.options[select.selectedIndex].value;
            console.log(email+"   "+tS);
            fetch("https://localhost:5001/Stanica/Brisi?email="+email+"&tS="+tS,{
                method: "DELETE"
            })
            .then(msg=>{
                if(msg.ok){
                    alert("Uspeh!")
                    return;
                }
                else{
                    alert("Neuspeh!")
                    return;
                }
            })
        };
    }
    crtajInput(host,labelHTML,tip,naziv){
        let label=document.createElement("label");
        label.innerHTML=labelHTML;
        host.appendChild(label);
        var input=document.createElement("input");
        input.type=tip;
        input.name=naziv;
        host.appendChild(input);
    }
    ucitajDestinacije(host){
        fetch("https://localhost:5001/Stanica/Destinacije?stanicaID="+this.id,{
            method: "GET"
        })
        .then(response=>{
            if(response.ok){
                response.json().then(destinacije=>{
                    let op;
                    let lista=[];
                    destinacije.forEach(destinacija=>{
                        let dest=new Destinacija(destinacija.id,destinacija.grad);
                        lista.push(dest);
                        op=document.createElement("option");
                        op.innerHTML=dest.grad;
                        op.value=dest.id;
                        host.appendChild(op);
                    })
                    this.ucitajTermine(lista[0].id);
                });
            }
        })
    }
    ucitajTermine(id){
        let dodajContainer=this.kont.querySelector(".dodaj");
        let select=document.createElement("select");
        select.className="termin";
        dodajContainer.appendChild(select);

        fetch("https://localhost:5001/Stanica/Termini?destinacijaID="+id,{
            method: "GET"
        })
        .then(response=>{
            if(response.ok){
                response.json().then(termini=>{
                    let op;
                    termini.forEach(termin=>{
                        let term=new Termini(termin.id,termin.vreme)
                        op=document.createElement("option");
                        op.innerHTML=term.vreme;
                        op.value=termin.id;
                        select.appendChild(op);
                    });
                    let button=document.createElement("button");
                    button.innerHTML="Dodaj Putovanje";
                    dodajContainer.appendChild(button);
                    button.onclick=(ev)=>{
                        let email=dodajContainer.querySelector("input[name='mejl']").value;
                        if(email===""){
                            alert("Unesi mejl");
                            return
                        }
                        let ime=dodajContainer.querySelector("input[name='ime']").value;
                        if(ime===""){
                            alert("Unesi ime");
                            return;
                        }
                        let prezime=dodajContainer.querySelector("input[name='prezime']").value;
                        if(prezime===""){
                            alert("Unesi prezime");
                            return;
                        }
                        let gID=dodajContainer.querySelector(".destinacije");
                        let dID=gID.options[gID.selectedIndex].value;//destinacija ID
                        let tID=dodajContainer.querySelector(".termin");
                        let terminID=tID.options[tID.selectedIndex].value;//termin ID
                        let zS=dodajContainer.querySelector(".tipSedista");
                        let tS=zS.options[zS.selectedIndex].innerHTML;//tip sedista
                        fetch("https://localhost:5001/Stanica/Putovanje?m="+email+"&i="+ime+"&p="+prezime
                        +"&tID="+terminID+"&dID="+dID+"&tS="+tS,{
                            method: "PUT"
                        })
                        .then(msg=>{
                            if(msg.ok){
                                alert("ok je!");
                                return;
                            }
                            else{
                                alert("pozdrav, lose...");
                                return;
                            }
                        })
                    }
                });
            }
        });
    }
}