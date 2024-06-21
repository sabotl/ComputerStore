import React, { useState } from 'react';
import ApiClient from './Api/ApiClient';
import logo from './logo.svg';
import './App.css';

function App() {
  const [products, setProducts] = useState([]);
  const [isLoaded, setIsLoaded] = useState(false);
  const apiClient = new ApiClient('https://localhost:7036/api'); 

  const fetchProducts = async () => {
    try {
      const fetchedProducts = await apiClient.getAllProducts();
      setProducts(fetchedProducts);
      setIsLoaded(true);
    } catch (error) {
      console.error('Error fetching products:', error);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <button onClick={fetchProducts}>Load Products</button> {/* Кнопка для загрузки продуктов */}
      </header>
      {isLoaded && ( // Показываем данные только если они загружены
        <main>
          <h1>Product List</h1>
          <ul>
            {products.map((product) => (
              <li key={product.id}>
                {product.productname} - {product.price}руб.
              </li>
            ))}
          </ul>
        </main>
      )}
    </div>
  );
}

export default App;