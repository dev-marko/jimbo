export const readFromLocalStorage = (key: string) => {
  if (typeof window === 'undefined') {
    return null;
  }

  return window.localStorage.getItem(key);
};

export const writeToLocalStorage = (key: string, value: string) => {
  window.localStorage.setItem(key, value);
};

export const removeFromLocalStorage = (key: string) => {
  window.localStorage.removeItem(key);
};
