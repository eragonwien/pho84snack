import { Injectable } from "@angular/core";

@Injectable()
export class HelperService {
  DEFAULT_IMAGE: string = "default";
  IMAGE_SQUARE = "512x512";
  IMAGE_16_9 = "1600x900";

  constructor() {}

  image(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + ".jpg";
  }

  imageSquare(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + "-" + this.IMAGE_SQUARE + ".jpg";
  }

  image16By9(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + "-" + this.IMAGE_16_9 + ".jpg";
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
