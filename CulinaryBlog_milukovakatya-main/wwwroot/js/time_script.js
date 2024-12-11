document.getElementById('time-min').addEventListener('input', updateTimeValues);
document.getElementById('time-max').addEventListener('input', updateTimeValues);

function updateTimeValues() {
    const timeMin = document.getElementById('time-min').value;
    const timeMax = document.getElementById('time-max').value;

    document.getElementById('time-values').innerText = `${timeMin} - ${timeMax}`;
}
const sortSelect = document.getElementById('sort-options');
const recipesContainer = document.querySelector('.container-recipes');

sortSelect.addEventListener('change', () => {
    const sortOption = sortSelect.value;

    const recipes = Array.from(recipesContainer.querySelectorAll('.recipes-item'));

    recipes.sort((a, b) => {
        switch (sortOption) {
            case 'time-asc': {
                const timeA = parseFloat(a.querySelector('table tr:nth-time(2) td:first-child').textContent.replace('$', ''));
                const timeB = parseFloat(a.querySelector('table tr:nth-time(2) td:first-child').textContent.replace('$', ''));
                return timeA - timeB;
            }
            case 'time-desc': {
                const timeA = parseFloat(a.querySelector('table tr:nth-time(2) td:first-child').textContent.replace('$', ''));
                const timeB = parseFloat(a.querySelector('table tr:nth-time(2) td:first-child').textContent.replace('$', ''));
                return timeB - timeA;
            }
            default: {
                location.reload();
            }
        }
    });
    recipes.forEach(recipes => recipesContainer.appendChild(recipes));

});
document.getElementById('apply-filter').addEventListener('click', () => {

    const idCategory = document.getElementById('categoryId').value;
    const timeMin = document.getElementById('time-min').value;
    const timeMax = document.getElementById('time-max').value;

    const filterData = {
        idCategory: idCategory,
        timeMin: timeMin,
        timeMax: timeMax,
    };
    console.log('Отправляемые данные:', filterData);

    fetch('/Recipes/Filter', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(filterData),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error('Ошибка при фильтрации данных');
            }
            return response.json();
        })
        .then((data) => {
            console.log('Результаты фильтрации:', data);

            dataDisplay(data);
        })
        .catch((errors) => {
            console.error('Ошибка', error);
        });
})
function dataDisplay(data) {

    const recipesList = document.querySelector('.list-recipes');
    recipesList.innerHTML = '';

    if (!data || data.length === 0) {
        const noRecipesMessage = `<h2>По данному фильтру нет рецептов</h2>`;
        recipesList.innerHTML = noRecipesMessage;
    } else {

        data.forEach((recipes) => {
            const recipesItem = ` 
                 <div class = "list-recipes">
                    <div class="recipes-item">
                        <div class="item-time">
                            Время приготовления: ${recipes.time} мин.
                        </div>
                       <img src="${recipes.pathImage}" class="item-recipes-img" />
                    
                        <div class="item-info">
                           <h2>${recipes.title}</h2>    
                           <h2>${recipes.description}</h2>
                        </div>
                    </div>
                        <input asp-for="@item.Id" value="${recipes.id}" style="display: none" />
                        <input asp-for="@item.CategoryId" value="${recipes.idCategory}" style="display: none" />
                 </div>
             `;
            recipesList.innerHTML += recipesItem;
        });
    }
}