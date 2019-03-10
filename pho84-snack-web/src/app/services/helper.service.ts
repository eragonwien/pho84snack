import { Injectable } from "@angular/core";

@Injectable()
export class HelperService {
  DEFAULT_IMAGE: string = "default";

  constructor() {}

  imagePath(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + ".jpg";
  }

  imageUrlPath(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "url('" + image + "')";
  }

  getPath(url: string): string {
    url = url.split(",").join("/");
    if (!url.startsWith("/")) {
      url = "/" + url;
    }
    return url;
  }
}
