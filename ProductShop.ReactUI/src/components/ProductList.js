import React, { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
import { getProducts } from './ProductData';
import './product.css';
import config from '../config';

const ProductList = () => {
    const [prod, setProduct] = useState([]);
    const [errors, setErrors] = useState([]);

    useEffect(() => {        
        getProducts().then(async res => {
            if(res.status !== 200) {
                setErrors(['Unable to retrieve products. Check the products API.', res.statusText]);
            } else {     
                const data = await res.json();          
                setErrors([]);                
                setProduct(data);
            }
        }).catch(err => setErrors(['Unable to retrieve products. Check the products API.']));
    }, [ setErrors, setProduct]);

    return (
        <div>
            {(errors && errors.length !== 0) &&
                <div className="err">
                    <ul>
                        {errors.map((error, index) => <li key={index}>{error}</li>)}
                    </ul>
                </div>
            }  
            <hr/>
            { (errors && errors.length == 0) && <div>
                <table>
                    <thead>
                        <tr>
                            <td colspan='2'></td>
                            <td>Id</td>
                            <td>Title</td>
                            <td>Description</td>
                            <td>Price</td>
                        </tr>
                    </thead>
                    <tbody>
                        {prod.map((product, index) => {
                            return (
                                <tr key={product.id}>
                                    <td>
                                        <Link to={`product/${product.id}`}>Edit</Link>
                                    </td>
                                    <td><img src={config.API_BASE_URI + product.imagePath} alt=''/></td>
                                    <td>{product.id}</td>
                                    <td>{product.title}</td>
                                    <td>{product.description}</td>
                                    <td>{product.price}</td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            </div> }
        </div>
    );
}

export default ProductList;