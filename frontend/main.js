window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const productionApiUrl = 'https://tokaazureresume.azurewebsites.net/api/GetResumeCounter?code=ghsXZht1byBS2OXQQoUSvfnlvWpTY3WNt9bqamBsHjlWAzFuYSCNnA==';
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