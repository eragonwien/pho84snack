import { Pipe, PipeTransform } from "@angular/core";

/*
 * Custom currency filter
 * Usage:
 *   price | cCurrency:currency:left:spacing
 * Example:
 *   {{ 2 | cCurrency:EUR:false:true}}
 *   formats to: 2 EUR
 */
@Pipe({
  name: "cCurrency"
})
export class CCurrencyPipe implements PipeTransform {
  transform(price: number, currency: string, left: boolean = false, spacing: boolean = false): any {
    let space: string = spacing ? " " : "";
    return left ? currency + space + price : price + space + currency;
  }
}
