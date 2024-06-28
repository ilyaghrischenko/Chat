function filterUsers() {
    const input = document.getElementById('username');
    const filter = input.value.toLowerCase();
    const resultsContainer = document.getElementById('results-container');
    const results = resultsContainer.getElementsByClassName('result-item');

    for (let i = 0; i < results.length; i++) {
        const button = results[i].getElementsByTagName('button')[0];
        const txtValue = button.textContent || button.innerText;
        if (txtValue.toLowerCase().indexOf(filter) > -1) {
            results[i].style.display = '';
        } else {
            results[i].style.display = 'none';
        }
    }
}