import config from '../config';

/**
 * saves the product
 */
export const saveProduct = async (dto) => {    
  const uri = `${config.API_BASE_URI}/api/product/save`;
  const res = await fetch(uri, {
      method: 'post',        
      headers: {
        'Content-Type': 'application/json',
      },        
      body: JSON.stringify(dto)
    });
    
  return res;
}

/**
 * @returns product by id
 */
export const loadProduct = async (id) => {    
    const uri = `${config.API_BASE_URI}/api/product/${id}`;
    const res = await fetch(uri);
    return res;
}

/**
 * @returns list of products
 */
export const getProducts = async () => {
  const uri = `${config.API_BASE_URI}/api/product/all`;
  const res = await fetch(uri);
  return res;
}