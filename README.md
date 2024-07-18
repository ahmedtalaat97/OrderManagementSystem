# OrderManagementSystem.Solution
<h1>Seeded Admin Account</h1>
<h2>userName : "Route"</h2>
<h2>Password : "AdminPassword123!"</h2>
<br>
<br>
<img src="https://github.com/user-attachments/assets/3ad6b80f-8ab9-4c32-917d-52d4e75e8522"  width="1000" height="500" />
<br>
Customer Endpoints:<br>
o POST /api/customers - Create a new customer<br>
o GET /api/customers/{customerId}/orders - Get all orders for a customer<br>
Order Endpoints:
o POST /api/orders - Create a new order<br>
o GET /api/orders/{orderId} - Get details of a specific order<br>
o GET /api/orders - Get all orders (admin only)<br>
o PUT /api/orders/{orderId}/status - Update order status (admin only)<br>
Product Endpoints:<br>
o GET /api/products - Get all products<br>
o GET /api/products/{productId} - Get details of a specific product<br>
o POST /api/products - Add a new product (admin only)<br>
o PUT /api/products/{productId} - Update product details (admin only)<br>

Invoice Endpoints:<br>
o GET /api/invoices/{invoiceId} - Get details of a specific invoice (admin
only)<br>
o GET /api/invoices - Get all invoices (admin only)<br>
User Endpoints:<br>
o POST /api/users/register - Register a new user<br>
o POST /api/users/login - Authenticate a user and return a JWT token<br>

