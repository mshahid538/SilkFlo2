﻿using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class DocumentationPresent : Abstract
  {
    private string _id = "";
    private string _colour = "";
    private string _name = "";
    private int _rating;
    private Decimal _weighting;

    public DocumentationPresent() => this._createdDate = new System.DateTime?(System.DateTime.Now);

    public override bool IsNew => string.IsNullOrWhiteSpace(this.Id);

    public string Id
    {
      get => this._id;
      set
      {
        value = value?.Trim();
        if (this._id == value)
          return;
        this._id = value;
        this.IsSaved = false;
      }
    }

    public string Colour
    {
      get => this._colour;
      set
      {
        value = value?.Trim();
        if (this._colour == value)
          return;
        this._colour = value;
        this.IsSaved = false;
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        value = value?.Trim();
        if (this._name == value)
          return;
        this._name = value;
        this.IsSaved = false;
      }
    }

    public int Rating
    {
      get => this._rating;
      set
      {
        if (this._rating == value)
          return;
        this._rating = value;
        this.IsSaved = false;
      }
    }

    public Decimal Weighting
    {
      get => this._weighting;
      set
      {
        if (this._weighting == value)
          return;
        this._weighting = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(DocumentationPresent documentationPresent)
    {
      this.Colour = documentationPresent.Colour;
      this.Name = documentationPresent.Name;
      this.Rating = documentationPresent.Rating;
      this.Weighting = documentationPresent.Weighting;
    }

    public override string ToString() => this.Name;
  }
}
