import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import ProductList from './components/ProductList';
import Product from './components/Product';

function App() {
  return (
    <>
      <div>
        <header>        
          <h1>Product Shop</h1>
          <h2>The best place to buy products</h2>
          <div>
            <Router>
                <Switch>
                  <Route exact path="/">
                    <ProductList />
                  </Route>
                  <Route path='/product/:id'>
                    <Product />
                  </Route>
                  <Route exact path="/products">
                    <ProductList />
                  </Route>
                </Switch>
            </Router>
          </div>
        </header>
      </div>
    </>
  );
}

export default App;
