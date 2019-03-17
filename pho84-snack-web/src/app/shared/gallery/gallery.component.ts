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

  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.gallery.forEach(g => {
      g.image = this.hs.imageSquare(g.image);
    });
    this.gallery = this.gallery.slice(0, this.count);
  }

  get count(): number {
    if (this.gallery.length <= this.itemPerRow || this.gallery.length % this.itemPerRow === 0) {
      return this.gallery.length;
    } else {
      return Math.floor(this.gallery.length / this.itemPerRow) * this.itemPerRow;
    }
  }
}
