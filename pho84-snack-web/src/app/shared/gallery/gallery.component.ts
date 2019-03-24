import { Component, OnInit, Input } from "@angular/core";
import { DataService } from "src/app/services/data.service";
import { HelperService } from "src/app/services/helper.service";
import { GalleryItem } from "src/app/models";

@Component({
  selector: "app-gallery",
  templateUrl: "./gallery.component.html",
  styleUrls: ["./gallery.component.css"]
})
export class GalleryComponent implements OnInit {
  @Input() gallery: GalleryItem[];
  @Input() itemPerRow: number = 3;
  @Input() placeholderImage: string;

  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.gallery = this.gallery.slice(0, this.count);
  }

  get count(): number {
    return Math.floor(this.gallery.length / this.itemPerRow) * this.itemPerRow;
  }

  image16by9(image: string) {
    return this.hs.image(image);
  }

  imageSquare(image: string) {
    return this.hs.imageSquare(image);
  }
}
