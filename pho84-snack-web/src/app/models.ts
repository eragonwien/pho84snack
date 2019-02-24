export interface Restaurant {
  name: string,
  description: string
}

export interface Contact {
  address1: string,
  address2: string,
  plz: number,
  city: string,
  phone: string,
  email: string,
  facebook: string,
  facebookUrl: string
}

export interface OpenHour {
  day: string,
  from: string,
  to: string,
  close: boolean
}

export interface Category {
  name: string,
  image: string,
  products: Product[]
}

export interface Menu {
  name: string,
  alias: string,
  description: string,
  price: string,
  currency: string,
  image: string,
  featured: boolean
  products: Product[]
}

export interface Product {
  name: string,
  alias: string,
  description: string,
  price: string,
  currency: string,
  image: string,
  featured: boolean
}

export interface Feature {
  id: number,
  title: string,
  subtitle: string,
  description: string,
  button: string,
  route: string,
  image: string,
  imageTitle: string,
  imageSubtitle: string,
  imageDescription: string,
  price: string,
  alignLeft: boolean
}