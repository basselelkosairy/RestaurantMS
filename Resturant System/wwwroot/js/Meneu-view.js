document.addEventListener("DOMContentLoaded", function () {
    const categories = window.categoriesData;

    document.querySelectorAll('.edit-btn').forEach(btn => {
        btn.addEventListener('click', function handleEdit() {
            const card = btn.closest('.card');
            const id = card.dataset.id;
            const name = card.querySelector('.name').textContent.trim();
            const price = card.querySelector('.price').textContent.replace('Price:', '').trim();
            const description = card.querySelector('.description').textContent.trim();
            const quantity = card.querySelector('.quantity').textContent.replace('Quantity:', '').trim();
            const categoryId = card.querySelector('.category').dataset.catId;

            card.querySelector('.name').innerHTML = `<input class="form-control name-input" value="${name}" />`;
            card.querySelector('.price').innerHTML = `<input type="number" class="form-control price-input" value="${price}" />`;
            card.querySelector('.description').innerHTML = `<input class="form-control description-input" value="${description}" />`;
            card.querySelector('.quantity').innerHTML = `<input class="form-control quantity-input" value="${quantity}" />`;
            card.querySelector('.image').outerHTML = `<input type="file" class="form-control image-input mt-2" />`;

            let categoryOptions = "";
            categories.forEach(c => {
                const selected = c.Value == categoryId ? "selected" : "";
                categoryOptions += `<option value="${c.Value}" ${selected}>${c.Text}</option>`;
            });
            card.querySelector('.category').innerHTML = `<select class="form-control category-input">${categoryOptions}</select>`;

            btn.textContent = "Save";
            btn.classList.remove('btn-primary');
            btn.classList.add('btn-success');
            btn.removeEventListener('click', handleEdit);

            btn.addEventListener('click', function handleSave() {
                const newName = card.querySelector('.name-input').value;
                const newPrice = card.querySelector('.price-input').value;
                const newDescription = card.querySelector('.description-input').value;
                const newquantity = card.querySelector('.quantity-input').value;
                const newCategoryId = card.querySelector('.category-input').value;
                const fileInput = card.querySelector('.image-input');

                const formData = new FormData();
                formData.append("Id", id);
                formData.append("Name", newName);
                formData.append("Price", newPrice);
                formData.append("Description", newDescription);
                formData.append("quantity", newquantity);
                formData.append("cateogryId", newCategoryId);


                if (fileInput && fileInput.files.length > 0) {
                    formData.append("ImageFile", fileInput.files[0]);
                }

                fetch('/Menue/Update', {
                    method: 'POST',
                    body: formData
                }).then(res => {
                    if (res.redirected) {
                        window.location.href = res.url;
                    } else {
                        alert("Something went wrong.");
                    }
                });
            }, { once: true });
        });
    });
});
