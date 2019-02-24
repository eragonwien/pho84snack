import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  gallery: string[];

  constructor(private ds: DataService) { }

  ngOnInit() {
    this.ds.gallery.subscribe((res: string[]) => {
      this.gallery = res.map(r => "/assets/images/compressed/" + r + ".jpg");
    });
  }

}
