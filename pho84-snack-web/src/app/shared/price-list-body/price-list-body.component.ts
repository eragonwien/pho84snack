import { Component, OnInit, Input } from "@angular/core";
import { Product, GalleryItem } from "src/app/models";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-price-list-body",
  templateUrl: "./price-list-body.component.html",
  styleUrls: ["./price-list-body.component.css"]
})
export class PriceListBodyComponent implements OnInit {
  @Input() products: Product[];
  @Input() placeholderImage: string;

  constructor(private hs: HelperService) {}

  ngOnInit() {}

  get isPrice(): boolean {
    return this.products && this.products.length > 0 && this.products[0].price && this.products[0].price !== 0;
  }
  get isPriceS(): boolean {
    return this.products && this.products.length > 0 && this.products[0].prices && this.products[0].prices !== 0;
  }
  get isPriceM(): boolean {
    return this.products && this.products.length > 0 && this.products[0].pricem && this.products[0].pricem !== 0;
  }
  get isPriceL(): boolean {
    return this.products && this.products.length > 0 && this.products[0].pricel && this.products[0].pricel !== 0;
  }
  get isPriceK(): boolean {
    return this.products && this.products.length > 0 && this.products[0].pricek && this.products[0].pricek !== 0;
  }

  get gallery(): GalleryItem[] {
    let gallery: GalleryItem[] = [];

    this.products.forEach(p => {
      if (p.image) {
        gallery.push({ image: p.image, text: p.name } as GalleryItem);
      }
    });
    return gallery;
  }
}
