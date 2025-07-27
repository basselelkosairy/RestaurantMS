var index = document.querySelectorAll('#order-items .order-item').length;

function addOrderItem() {
    var container = document.getElementById("order-items");

    var html = `
        <div class="order-item">
            <div class="item-row">
                <div class="form-group">
                    <label>Item Name *</label>
                    <select required name="OrderItems[${index}].MenuItemId" class="form-control">
                        <option value="">-- Select Item --</option>
                        ${menuItemsOptions}
                    </select>
                </div>

                <div class="form-group">
                    <label>Notes</label>
                    <textarea name="OrderItems[${index}].Notes" class="form-control"></textarea>
                </div>

                <div class="form-group">
                    <label>Quantity *</label>
                    <input required name="OrderItems[${index}].Quantity" class="form-control quantity-input" />
                </div>

         <div >
    <button class="delete-btn" onclick="Delete(this)">Delete</button>
</div>       
            </div>
        </div>`;

    container.insertAdjacentHTML("beforeend", html);
    index++;
}
function Delete(button) {

    button.closest('.order-item').remove();

}


