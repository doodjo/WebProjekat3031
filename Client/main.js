import {Stanica} from "./Stanica.js";




// listaTipovaSedista=[];
// fetch("https://localhost:5001/Putnik/PreuzmiTipoveSedista")
//     .then(p=>{
//         p.json().then(tipovi=>{
//             tipovi.forEach(tip => {
//                 if(!listaTipovaSedista.includes(tip))
//                  listaTipovaSedista.push(tip);
//             })                                   LEGACY UVREDA!!!KO PITA.....

fetch("https://localhost:5001/Stanica/Stanica",{
    method: "GET"
})
.then(response=>{
    if(response.ok){
        response.json().then(stanice=>{
                stanice.forEach(obj=>{
                let stanica=new Stanica(obj.id,obj.ime);
                stanica.crtaj();
            });
        })
    }
    else{
        alert("Malo stanica....velika tuga....");
    }
});