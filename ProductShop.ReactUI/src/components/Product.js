import React, { useState, useEffect } from 'react';
import { useParams, Link } from "react-router-dom";
import { loadProduct, saveProduct } from './ProductData';
import './product.css';
import config from "../config";

function Product() {
    const { id } = useParams();
    const [prod, setProd] = useState({
        id: id,
        description: '',
        price: null,
        imagePath: '',
        title: '',
    });
    const [errors, setErrors] = useState([]);

    useEffect(() => {        
        loadProduct(id).then(async res => {
            if(res.status !== 200) {
                setErrors(['Error in loading the prod details', res.statusText]);                
            } else {     
                const jsonRes = await res.json();   
                if(jsonRes?.success) { 
                    const data = jsonRes.data;
                    setProd(data);
                } else {
                    setErrors(...jsonRes?.errors);
                }
            }
        })
    }, [ setErrors, setProd, id]);

    const updateProduct = (key, value) => {
        if(prod) {
            const updatedProduct = {...prod, [key]: value};
            setProd(updatedProduct);
        }

        if(errors && errors.length !== 0) {
            setErrors([]);
        }
    };

    const save = () => {
        if(prod?.id) {
            saveProduct(prod).then(async res => {
                if(res.status !== 200) {
                    setErrors(['Failed to save the prod', res.statusText]);                
                } else {     
                    const jsonRes = await res.json();   
                    if(jsonRes?.success) {                                      
                        setErrors(['1 prod updated']);
                    } else {
                        setErrors(...jsonRes?.errors);
                    }
                }
            })
        }
    };

    return (
        <div>
            <h2> Edit Product : { id }</h2>
            {(errors && errors.length !== 0) &&
                <div className="err">
                    <ul>
                        {errors.map((error, index) => <li key={index}>{error}</li>)}
                    </ul>
                </div>
            }            
            <hr/>

            <div className='frm'>
                <img src={config.API_BASE_URI + prod.imagePath} className="img large" alt=''/>
                <fieldset>
                    <label>Title</label>
                    <input type='text' name='title' minLength='1' 
                        id='title' value={prod?.title || ''} 
                        onChange={(event) => updateProduct('title', event.target.value )} />
                </fieldset>
                <fieldset>
                    <label>Description</label>
                    <textarea type='text' name='description' minLength='5' rows='5' 
                        id='description' value={prod?.description || ''} 
                        onChange={(event) => updateProduct('description', event.target.value)} />
                </fieldset>
                <fieldset>
                    <label>Price</label>
                    <input type='number' name='price' minLength='5' 
                        id='price' value={prod?.price || 0} 
                        onChange={(event) => updateProduct('price', event.target.value )} />
                </fieldset>
                <fieldset>
                    <label>Image Path</label>
                    <input type='text' name='title' minLength='1'
                           id='title' value={prod?.imagePath || ''}
                           onChange={(event) => updateProduct('title', event.target.value )} />
                </fieldset>
                <hr/>
                <div>
                    <Link to='/'>[All Products]</Link>
                    <button type='submit'onClick={save}>Save Product</button>
                </div>
            </div>
        </div>
    );
}

export default Product;