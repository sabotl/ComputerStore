import React, { useState } from 'react';

const productList = [
    { id: 1, name: 'Apple', category: 'Fruit', price: 1 },
    { id: 2, name: 'Bread', category: 'Bakery', price: 2 },
    { id: 3, name: 'Carrot', category: 'Vegetable', price: 1 },
    { id: 4, name: 'Milk', category: 'Dairy', price: 2 }
];

function Products() {
    const [filter, setFilter] = useState('');
    const [products, setProducts] = useState(productList);

    const handleFilterChange = (e) => {
        setFilter(e.target.value);
        if (e.target.value === '') {
            setProducts(productList);
        } else {
            setProducts(productList.filter(product => product.category.toLowerCase().includes(e.target.value.toLowerCase())));
        }
    };

    return (
        <div className="products container">
            <input type="text" value={filter} onChange={handleFilterChange} placeholder="Filter by category" />
            {products.map(product => (
                <div key={product.id} className="product">
                    <h3>{product.name}</h3>
                    <p>Category: {product.category}</p>
                    <p>Price: ${product.price}</p>
                </div>
            ))}
        </div>
    );
}

export default Products;