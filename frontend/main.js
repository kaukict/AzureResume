window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const productionApiUrl = 'https://azresumetoka.azurewebsites.net/api/GetResumeCounter?code=hbDy4IvTcHrD3W-zFfeP9dvHBaQJuKW0-nk7P8sPf0hyAzFuThxFhQ==';
const functionUrl = 'http://localhost:7071/api/GetResumeCounter';

const getVisitCount = () => {
    let count = 30;
    fetch(productionApiUrl).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website called function API.");
        count =  response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}