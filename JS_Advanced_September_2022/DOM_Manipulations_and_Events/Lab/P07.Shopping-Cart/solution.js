function solve() {
   let shopingCart = document.querySelector('.shopping-cart');
   let textArea = document.querySelector('textarea');
   shopingCart.addEventListener('click', shop);
   let totalPrice = 0;
   let products = [];
   let checkedOut = false;

   function shop(event) {
      if(event.target.nodeName !== 'BUTTON'){
         return;
      }

      if (checkedOut) {
         return;
      }

      if(event.target.className === 'checkout'){
         textArea.textContent += `You bought ${products.join(', ')} for ${totalPrice.toFixed(2)}.`;

         checkedOut = true;
         return;
      }

      let productName = event.target.parentElement.previousElementSibling.querySelector('.product-title').textContent;
      let productPrice = Number(event.target.parentElement.nextElementSibling.textContent);
      totalPrice += productPrice;

      if(!products.includes(productName)){
         products.push(productName);
      }

      textArea.textContent += `Added ${productName} for ${productPrice.toFixed(2)} to the cart.\n`;
   }
}